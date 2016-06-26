using System;

namespace MassivePixel.Clay
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    class MapClassAttribute : Attribute
    {
        public Type Target { get; }

        public Type Source { get; }

        public MapClassAttribute(Type target, Type source)
        {
            Target = target;
            Source = source;
        }
    }
}
