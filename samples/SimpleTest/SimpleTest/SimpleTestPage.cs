using Xamarin.Forms;

namespace SimpleTest
{
    public class SimpleTestPage : ContentPage
    {
        public SimpleTestPage()
        {
            Content = new SimpleTestView();

            // TODO: move explicit methods to lifecycle events
            ((MassivePixel.Clay.ClayView)Content).InitialRender();
        }
    }
}
