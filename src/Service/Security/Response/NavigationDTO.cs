using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Portolo.Security.Response
{
    [XmlRoot("Navigation")]
    public class NavigationDTO
    {
        [XmlIgnore]
        public string Updated { get; set; }

        [XmlIgnore]
        public bool PriviousState { get; set; }

        [XmlElement("NavigationID")]
        public int NavigationID { get; set; }

        [XmlElement("ApplicationID")]
        public int ApplicationID { get; set; }

        [XmlIgnore]
        public string ApplicationName { get; set; }

        [XmlElement("MenuName")]
        public string MenuName { get; set; }

        [XmlElement("MenuText")]
        public string MenuText { get; set; }

        [XmlElement("Sequence")]
        public int Sequence { get; set; }

        [XmlElement("Status")]
        public string Status { get; set; }

        [XmlElement("Type")]
        public string Type { get; set; }

        [XmlElement("HelpID")]
        public int? HelpID { get; set; }

        [XmlIgnore]
        public string HelpDescription { get; set; }

        [XmlElement("Command")]
        public string Command { get; set; }

        [XmlElement("Link")]
        public string Link { get; set; }

        [XmlElement("Icon")]
        public string Icon { get; set; }

        [XmlElement("ParentID")]
        public int ParentID { get; set; }

        [XmlElement("CreatedBy")]
        public int? CreatedBy { get; set; }

        [XmlElement("CreatedOn")]
        public DateTime? CreatedOn { get; set; }

        [XmlElement("ModifiedBy")]
        public int? ModifiedBy { get; set; }

        [XmlElement("ModifiedOn")]
        public DateTime? ModifiedOn { get; set; }

        [XmlArray("NavigationItem")]
        [XmlArrayItem("Item")]
        public List<NavigationItemDTO> NavigationItems { get; set; }
    }
}
