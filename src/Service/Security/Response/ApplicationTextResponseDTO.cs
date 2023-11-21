using System;

namespace Portolo.Security.Response
{
    public class ApplicationTextResponseDTO
    {
        public int? ApplicationTextKey { get; set; }
        public string ApplicationTextDesc { get; set; }
        public string SettingValue { get; set; }
        public bool? UserEditable { get; set; }
        public string SiteName { get; set; }
        public string Status { get; set; }
        public string SLNo { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

    }
}
