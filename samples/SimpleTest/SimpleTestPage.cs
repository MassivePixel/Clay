using MassivePixel.Clay;
using Xamarin.Forms;

namespace SimpleTest
{
    public class SimpleTestPage : ContentPage, IClayComponent
    {
        Xamarin.Forms.Shadow.View vdom;

        public SimpleTestPage()
        {
            vdom = Render();
            Content = vdom.Create();
        }

        public Xamarin.Forms.Shadow.View Render()
        {
            return new Xamarin.Forms.Shadow.Label
            {
                Text = "Welcome to Xamarin Forms!",
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };
        }
    }
}

