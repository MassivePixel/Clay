using System;

namespace MassivePixel.Clay
{
    public class Attr
    {
        public string PropertyName { get; }

        public object Value { get; }

        public Type TargetType { get; }

        public Attr(string propertyName, object value, Type targetType)
        {
            PropertyName = propertyName;
            Value = value;
            TargetType = targetType;
        }
    }
}

