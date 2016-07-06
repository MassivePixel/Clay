using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MassivePixel.Clay
{
    public class Node
    {
        public string Name { get; }
        public IReadOnlyDictionary<string, object> Properties { get; }
        public IReadOnlyList<Node> Children { get; }

        public List<Attr> Attributes { get; } = new List<Attr>();

        public Node(string name, Dictionary<string, object> properties = null, IEnumerable<Node> children = null)
        {
            Name = name;
            Properties = properties ?? new Dictionary<string, object>();
            Children = children?.ToList() ?? new List<Node>();
        }

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
