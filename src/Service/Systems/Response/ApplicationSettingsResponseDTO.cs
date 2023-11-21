using System;

namespace Portolo.Systems.Response
{
    public class ApplicationSettingsResponseDTO
    {
        public int? ApplicationSettingKey { get; set; }
        public string ApplicationSettingDesc { get; set; }
        public string SettingValue { get; set; }
        public bool? UserEditable { get; set; }
        public string Usage { get; set; }
        public string SiteName { get; set; }
        public string SLNo { get; set; }
        public string Status { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

    }
}
