using MassivePixel.Clay;
using Xamarin.Forms;

namespace CounterSample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new ContentPage();
            VirtualDOM.Render(new Counter(), MainPage);
        }
    }
}
