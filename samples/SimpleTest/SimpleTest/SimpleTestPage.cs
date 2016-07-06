using MassivePixel.Clay;
using Xamarin.Forms;

namespace SimpleTest
{
    public class SimpleTestPage : ContentPage
    {
        SimpleTestView view;

        public SimpleTestPage()
        {
            view = new SimpleTestView();
            Content = VirtualDOM.Render(view.Render());
        }
    }
}
