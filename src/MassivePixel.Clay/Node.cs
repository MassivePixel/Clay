using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MassivePixel.Clay
{
    public class Node
    {
        public List<Attr> Attributes { get; } = new List<Attr>();

        public T Get<T>([CallerMemberName] string propertyName = null)
        {
            var attr = Attributes.FirstOrDefault(x => x.PropertyName == propertyName);
            if (attr.PropertyName == propertyName)
                return (T)attr.Value;
            return default(T);
        }

        public void Set<T>(Type targetType, T value, [CallerMemberName] string propertyName = null)
        {
            Attributes.RemoveAll(x => x.PropertyName == propertyName);
            Attributes.Add(new Attr(propertyName, value, targetType));
        }
    }
}
