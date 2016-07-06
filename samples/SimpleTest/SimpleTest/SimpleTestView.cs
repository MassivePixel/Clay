using System.Windows.Input;
using MassivePixel.Clay;
using Xamarin.Forms;
using static MassivePixel.Clay.VirtualDOM;

namespace SimpleTest
{
    public class SimpleTestView : ClayComponent
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

        public override Node Render()
            => h(new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Children =
                {
                    new Label
                    {
                        Text = "Welcome to Xamarin Forms!",
                        HorizontalOptions = LayoutOptions.Center
                    },
                    new Button
                    {
                        Text = "Click me",
                        Command = ClickCommand
                    }
                }
            });
    }
}
