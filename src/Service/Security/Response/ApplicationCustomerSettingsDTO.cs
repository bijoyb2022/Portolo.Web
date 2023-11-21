using System.Collections.Generic;
using System.Xml.Serialization;

namespace Portolo.Security.Response
{
    [XmlRoot("ApplicationSettings")]
    public class ApplicationCustomerSettingsDTO
    {
        [XmlArray("Settings")]
        [XmlArrayItem("Item")]
        public List<ApplicationCustomerSettingDTO> ApplicationCustomerSetting { get; set; }

        [XmlIgnore]
        public int CreatedBy { get; set; }
    }
}
