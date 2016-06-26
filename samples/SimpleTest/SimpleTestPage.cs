using System.Windows.Input;
using MassivePixel.Clay;
using Xamarin.Forms;

namespace SimpleTest
{
    public class SimpleTestPage : ContentPage, IClayComponent
    {
        Xamarin.Forms.Shadow.View vdom;
        ICommand ClickCommand;

        public SimpleTestPage()
        {
            ClickCommand = new Command(Click);

            vdom = Render();
            Content = vdom.Create();
        }

        void Click()
        {
            DisplayAlert("alert", "hello world", "ok");
        }

        public Xamarin.Forms.Shadow.View Render()
        {
            return new Xamarin.Forms.Shadow.StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Children =
                {
                    new Xamarin.Forms.Shadow.Label
                    {
                        Text = "Welcome to Xamarin Forms!",
                        HorizontalOptions = LayoutOptions.Center
                    },
                    new Xamarin.Forms.Shadow.Button
                    {
                        Text = "Click me",
                        Command = ClickCommand
                    }
                }
            };
        }
    }
}

