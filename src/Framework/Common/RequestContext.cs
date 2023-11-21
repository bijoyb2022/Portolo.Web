using System.Collections.Generic;

namespace Portolo.Framework.Common
{
    public class RequestContext<T>
    {
        public T Item { get; set; }
        public IEnumerable<T> Items { get; set; }
        public string DbConnection { get; set; }
    }
}