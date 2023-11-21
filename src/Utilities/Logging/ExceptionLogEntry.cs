using System;

namespace Portolo.Utility.Logging
{
    public class ExceptionLogEntry : LogEntry
    {
        public Exception Exception { get; set; }
    }
}
