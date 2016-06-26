using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MassivePixel.Clay;

[assembly: MapClass(typeof(Xamarin.Forms.StackLayout), typeof(Xamarin.Forms.Shadow.StackLayout))]
[assembly: MapClass(typeof(Xamarin.Forms.Label), typeof(Xamarin.Forms.Shadow.Label))]
[assembly: MapClass(typeof(Xamarin.Forms.Button), typeof(Xamarin.Forms.Shadow.Button))]

namespace MassivePixel.Clay
{
    public static class VirtualDomRenderer
    {
        private static List<MapClassAttribute> mappers;

        static VirtualDomRenderer()
        {
            mappers = typeof(VirtualDomRenderer)
                .GetTypeInfo()
                .Assembly
                .GetCustomAttributes()
                .OfType<MapClassAttribute>()
                .ToList();
        }

        public static Xamarin.Forms.View Create(this Xamarin.Forms.Shadow.View root)
        {
            var mapper = mappers.FirstOrDefault(x => x.Source == root.GetType());
            if (mapper != null)
            {
                var actual = Activator.CreateInstance(mapper.Target);

                foreach (var attr in root.Attributes)
                {
                    if (mapper.Target == attr.TargetType ||
                        mapper.Target.GetTypeInfo().IsSubclassOf(attr.TargetType))
                    {
                        var prop = actual.GetType().GetRuntimeProperty(attr.PropertyName);
                        prop?.SetValue(actual, attr.Value);
                    }
                }

                if (root.ActualChildren?.Any() == true)
                {
                    var layout = actual as Xamarin.Forms.Layout<Xamarin.Forms.View>;
                    if (layout != null)
                    {
                        foreach (var child in root.ActualChildren)
                            layout.Children.Add(Create(child));
                    }
                }

                return actual as Xamarin.Forms.View;
            }

            return null;
        }

        public static Patch Diff(this Xamarin.Forms.Shadow.View old, Xamarin.Forms.Shadow.View newer)
        {
            var patch = new Patch();

            return patch;
        }

        public static Xamarin.Forms.View Patch(this Xamarin.Forms.View root, Patch patch)
        {
            return root;
        }
    }
}

