using System;

namespace Portolo.Security.Request
{
    public class ApplicationSettingsRequestDTO
    {
        public int? ApplicationSettingKey { get; set; }
        public string ApplicationSettingDesc { get; set; }
        public string OptType { get; set; }

    }
}
