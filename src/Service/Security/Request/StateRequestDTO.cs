using System;

namespace Portolo.Security.Request
{
    public class StateRequestDTO
    {
        public int? StateKey { get; set; }
        public int? CountryKey { get; set; }
        public string StateCode { get; set; }
        public string StateName { get; set; }
        public string OptType { get; set; }

    }
}
