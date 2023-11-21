using System.Collections.Generic;

namespace Portolo.Framework.Common
{
    public enum ResponseStatus
    {
        Error = 0,
        Warning = 1,
        Success = 2
    }

    public class ResponseContext<T>
    {
        public ResponseStatus Status { get; set; }
        public string Message { get; set; }
        public T Item { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}