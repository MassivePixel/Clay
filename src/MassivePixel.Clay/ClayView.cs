using System;

namespace MassivePixel.Clay
{
    public abstract class ClayView : Xamarin.Forms.ContentView
    {
        Xamarin.Forms.Shadow.View vdom;

        public ClayView()
        {
        }

        public void InitialRender()
        {
            vdom = Render();
            Content = vdom.Create();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            ReRender();
        }

        public abstract Xamarin.Forms.Shadow.View Render();

        private void ReRender()
        {
            Content.Patch(vdom.Diff(Render()));
        }
    }
}

