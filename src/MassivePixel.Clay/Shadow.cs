using System.Collections.Generic;
using System.Windows.Input;
using MassivePixel.Clay;

namespace Xamarin.Forms.Shadow
{
    public abstract class View : Node
    {
        public View content { get; protected set; }
        protected List<View> children;
        public IReadOnlyList<View> ActualChildren => children;

        public Xamarin.Forms.LayoutOptions? HorizontalOptions
        {
            get { return Get<Xamarin.Forms.LayoutOptions?>(); }
            set { Set(typeof(Xamarin.Forms.View), value); }
        }

        public Xamarin.Forms.LayoutOptions? VerticalOptions
        {
            get { return Get<Xamarin.Forms.LayoutOptions?>(); }
            set { Set(typeof(Xamarin.Forms.View), value); }
        }
    }

    public class StackLayout : View
    {
        public List<View> Children
        {
            get { return children = children ?? new List<View>(); }
            set { children = value; }
        }
    }

    public class ContentView : View
    {
        public View Content { get { return content; } set { content = value; } }
    }

    public class Frame : ContentView
    {
    }

    public class Label : View
    {
        public string Text
        {
            get { return Get<string>(); }
            set { Set(typeof(Xamarin.Forms.Label), value); }
        }
    }

    public class Button : View
    {
        public string Text
        {
            get { return Get<string>(); }
            set { Set(typeof(Xamarin.Forms.Button), value); }
        }

        public ICommand Command
        {
            get { return Get<ICommand>(); }
            set { Set(typeof(Xamarin.Forms.Button), value);}
        }
    }
}
