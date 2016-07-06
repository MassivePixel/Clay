using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;

namespace MassivePixel.Clay
{
    public static class VirtualDOM
    {
        static Type[] Types =
        {
            typeof(Button),
            typeof(Label),
            typeof(StackLayout)
        };

        static VirtualDOM()
        {
        }

        public static Node h(string name, Dictionary<string, object> properties = null, params Node[] children)
        {
            return new Node(name, properties, children);
        }

        public static Node h<TProps>(string name, TProps propObject, params Node[] children)
        {
            return new Node(name, GetProperties(propObject), children);
        }

        public static Node h<TView>(Dictionary<string, object> properties = null, params Node[] children) where TView : View
        {
            return h(typeof(TView).Name, properties, children);
        }

        public static Node h(View view, params Node[] children)
        {
            var properties = GetViewProperties(view);
            return h(view.GetType().Name, properties,
                     GetChildren(view).Union(children ?? new Node[0]).ToArray());
        }

        public static View Render(Node rootNode)
        {
            if (rootNode == null)
                throw new ArgumentNullException(nameof(rootNode));

            var rootDOM = CreateFrom(rootNode);

            foreach (var property in rootNode.Properties)
            {
                if (property.Key.Contains("."))
                {
                    var owner = property.Key.Substring(0, property.Key.IndexOf('.'));
                    var propname = property.Key.Substring(property.Key.IndexOf('.') + 1);
                    var ownertype = typeof(View).GetTypeInfo()
                                                .Assembly
                                                .DefinedTypes
                                                .FirstOrDefault(t => t.Name == owner);
                    if (ownertype != null)
                    {
                        var method = ownertype.GetDeclaredMethod("Set" + propname);
                        if (method != null)
                        {
                            method.Invoke(null, new[] { rootDOM, property.Value });
                        }
                    }
                }
                else
                {
                    var prop = rootDOM.GetType().GetRuntimeProperty(property.Key);
                    if (prop != null && property.Value != null)
                    {
                        if (prop.PropertyType.GetTypeInfo().IsAssignableFrom(property.Value.GetType().GetTypeInfo()))
                            prop.SetValue(rootDOM, property.Value);
                    }
                }
            }

            if (rootNode.Children.Any())
            {
                if (rootDOM is ContentView)
                {
                    ((ContentView)rootDOM).Content = Render(rootNode.Children.First());
                }
                else if (rootDOM is Layout<View>)
                {
                    foreach (var child in rootNode.Children)
                    {
                        ((Layout<View>)rootDOM).Children.Add(Render(child));
                    }
                }
            }

            return rootDOM;
        }

        public static void Render(ClayComponent component, VisualElement parent)
        {
            Action<View> mounter = _ => { };
            if (parent is ContentPage)
                mounter = view => ((ContentPage)parent).Content = view;

            if (mounter != null)
                mounter(Render(component.Render()));

            component.Changed += (_, __) => mounter(Render(component.Render()));
        }

        public static View CreateFrom(Node node)
        {
            var name = node.Name;

            var type = Types.FirstOrDefault(t => t.Name == name);
            if (type == null)
                throw new ArgumentException(nameof(name));

            return Activator.CreateInstance(type) as View;
        }

        public static Dictionary<string, object> GetProperties<TProps>(TProps obj)
        {
            var props = new Dictionary<string, object>();

            if (obj != null)
            {
                foreach (var prop in obj.GetType().GetRuntimeProperties())
                    props.Add(prop.Name, prop.GetValue(obj));
            }

            return props;
        }

        public static Dictionary<string, object> GetViewProperties(View view)
        {
            if (view == null) return null;

            var props = new Dictionary<string, object>();
            var def = Activator.CreateInstance(view.GetType());
            var type = def.GetType().Name;
            foreach (var property in def.GetType().GetRuntimeProperties())
            {
                if (!property.CanRead || !property.CanWrite ||
                    property.GetMethod.IsStatic ||
                    !property.GetMethod.IsPublic ||
                    !property.SetMethod.IsPublic ||
                    property.Name == "Parent")
                    continue;

                try
                {
                    var d = property.GetValue(def);
                    var n = property.GetValue(view);

                    if ((d == null && n != null) ||
                        (d != null && !d.Equals(n)))
                        props.Add(property.Name, n);
                }
                catch { }
            }
            return props;
        }

        public static IEnumerable<Node> GetChildren(View view)
        {
            var children = new List<Node>();
            if (view is ContentView)
            {
                var content = ((ContentView)view).Content;
                if (content != null)
                    children.Add(h(content));
            }
            else if (view is Layout<View>)
            {
                var layout = (Layout<View>)view;
                children.AddRange(layout.Children.Select(c => h(c)));
            }
            return children;
        }
    }
}
