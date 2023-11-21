using System;
using System.Xml.Serialization;

namespace Portolo.Security.Response
{
    public class ApplicationCustomerSettingDTO
    {
        [XmlElement("OwnerID")]
        public int OwnerId { get; set; }

        [XmlElement("CustomerKey")]
        public int CustomerKey { get; set; }

        [XmlElement("SettingsID")]
        public int SettingsID { get; set; }

        [XmlElement("Value")]
        public string Value { get; set; }

        public string ModuleName { get; set; }
        public string SettingGroup { get; set; }
        public string SettingsCategory { get; set; }
        public string SettingsType { get; set; }
        public string SettingsName { get; set; }
        public string SettingsDescription { get; set; }
        public string DefaultValue { get; set; }
        public string Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
