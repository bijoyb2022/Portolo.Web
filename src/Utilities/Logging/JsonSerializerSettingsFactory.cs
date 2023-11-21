using Newtonsoft.Json;

namespace Portolo.Utility.Logging
{
    internal static class JsonSerializerSettingsFactory
    {
        private static JsonSerializerSettings SerializerSettings { get; set; }

        public static JsonSerializerSettings Create()
        {
            if (SerializerSettings == null)
            {
                SerializerSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                };
            }

            return SerializerSettings;
        }
    }
}
