using System;

namespace MassivePixel.Clay
{
    public class ClayComponent
    {
        public event EventHandler Changed;

        protected void RaiseChanged() => Changed?.Invoke(this, EventArgs.Empty);

        public virtual Node Render()
        {
            return null;
        }
    }

    public class ClayComponent<T> : ClayComponent
    {
        public T State { get; set; }

        public void SetState(T state)
        {
            State = state;
            RaiseChanged();
        }
    }
}
