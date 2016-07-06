using MassivePixel.Clay;
using Xamarin.Forms;
using static MassivePixel.Clay.VirtualDOM;

namespace CounterSample
{
    public class Counter : ClayComponent<int>
    {
        public override Node Render()
            => h(new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Children =
                {
                    new Label
                    {
                        Text = $"Current value = {State}"
                    },
                    new Button
                    {
                        Text = "+",
                        Command = new Command(inc)
                    }
                }
            });

        void inc()
        {
            SetState(State + 1);
        }
    }
}