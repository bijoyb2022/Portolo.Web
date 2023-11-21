using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portolo.Systems.Data
{
    [Table("ApplicationText")]
    public partial class ApplicationText
    {
        public int? ApplicationTextKey { get; set; }
        public string ApplicationTextDesc { get; set; }
        public string SettingValue { get; set; }
        public bool? UserEditable { get; set; }
        public string SiteName { get; set; }
        public string Status { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
