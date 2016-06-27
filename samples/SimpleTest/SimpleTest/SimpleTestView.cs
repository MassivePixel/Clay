using System.Windows.Input;
using MassivePixel.Clay;
using Xamarin.Forms;

namespace SimpleTest
{
    public class SimpleTestView : ClayView
    {
        readonly ICommand ClickCommand;

        public SimpleTestView()
        {
            ClickCommand = new Command(Click);
        }

        void Click()
        {
            Application.Current.MainPage.DisplayAlert("alert", "hello world", "ok");
        }

        public override Xamarin.Forms.Shadow.View Render()
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
