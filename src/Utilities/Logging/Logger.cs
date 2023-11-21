using Newtonsoft.Json;
using Portolo.Utility.Configuration;
using Serilog;

namespace Portolo.Utility.Logging
{
    public class Logger
    {
        private readonly string loggerName;

        static Logger()
        {
            Serilog.Log.Logger = new LoggerConfiguration()
                .ReadFrom
                .Configuration(ConfigurationUtility.Current.Configuration)
                .CreateLogger();
        }

        public Logger(string loggerName)
        {
            this.loggerName = loggerName;
        }

        public void Log(LogEntry logEntry)
        {
            var preparedLogEntry = this.PrepareLogEntry(logEntry);
            switch (logEntry.LogLevel)
            {
                case LogLevel.Debug:
                    Serilog.Log.Debug(preparedLogEntry);
                    break;

                case LogLevel.Informational:
                    Serilog.Log.Information(preparedLogEntry);
                    break;

                case LogLevel.Warning:
                    Serilog.Log.Warning(preparedLogEntry);
                    break;

                case LogLevel.Error:
                    Serilog.Log.Error(preparedLogEntry);
                    break;

                case LogLevel.None:
                    break;
            }
        }

        private string PrepareLogEntry(LogEntry logEntry)
        {
            // allows the ability to query/group on a specific logger
            logEntry.LoggerName = logEntry.LoggerName ?? this.loggerName;

            return JsonConvert.SerializeObject(logEntry, JsonSerializerSettingsFactory.Create());
        }
    }
}
