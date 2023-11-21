using System.Collections.Generic;
using System.Configuration;

namespace Portolo.Framework.Common
{
    public enum Bindings
    {
        wsHttpBinding,
        basicHttpBinding,
        netTcpBinding
    }

    public class GenericConfigurationElementCollection<T> : ConfigurationElementCollection, IEnumerable<T>
        where T : ConfigurationElement, new()
    {
        private readonly List<T> elements = new List<T>();

        public new IEnumerator<T> GetEnumerator() => this.elements.GetEnumerator();

        protected override ConfigurationElement CreateNewElement()
        {
            var newElement = new T();
            this.elements.Add(newElement);
            return newElement;
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return this.elements.Find(e => e.Equals(element));
        }
    }
}