using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Portolo.Utility.Logging
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum LogLevel
    {
        None = 0,

        Debug = 1,

        Informational = 2,

        Warning = 3,

        Error = 4
    }
}
