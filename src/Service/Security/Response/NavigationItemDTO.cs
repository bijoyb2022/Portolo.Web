using System;
using System.Xml.Serialization;

namespace Portolo.Security.Response
{
    public class NavigationItemDTO
    {
        [XmlIgnore]
        public string RowUpdated { get; set; }

        [XmlIgnore]
        public bool PriviousState { get; set; }

        [XmlElement("NavigationItemID")]
        public int NavigationItemID { get; set; }

        [XmlElement("NavigationID")]
        public int NavigationID { get; set; }

        [XmlElement("MenuItemName")]
        public string MenuItemName { get; set; }

        [XmlElement("MenuItemText")]
        public string MenuItemText { get; set; }

        [XmlElement("Sequence")]
        public int Sequence { get; set; }

        [XmlElement("Status")]
        public string Status { get; set; }

        [XmlElement("Command")]
        public string Command { get; set; }

        [XmlElement("Link")]
        public string Link { get; set; }

        [XmlElement("Icon")]
        public string Icon { get; set; }

        [XmlElement("CreatedBy")]
        public int? CreatedBy { get; set; }

        [XmlElement("CreatedOn")]
        public DateTime? CreatedOn { get; set; }

        [XmlElement("ModifiedBy")]
        public int? ModifiedBy { get; set; }

        [XmlElement("ModifiedOn")]
        public DateTime? ModifiedOn { get; set; }
    }
}
