using System;

namespace Portolo.Security.Request
{
    public class CountryTimeZoneRequestDTO
    {
        public int? CountryTimeZoneKey { get; set; }
        public int? CountryKey { get; set; }
        public string CountryCode { get; set; }
        public string OptType { get; set; }

    }
}
