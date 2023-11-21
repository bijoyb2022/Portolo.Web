using System.Collections.Generic;

namespace Portolo.Utility.Logging
{
    public class LogEntry
    {
        public virtual LogLevel LogLevel { get; set; }

        public string LogEntryType => this.GetType().FullName;

        public string LoggerName { get; set; }

        public string Message { get; set; }

        public IDictionary<string, string> Metadata { get; set; }
    }
}
