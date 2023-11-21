using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Portolo.Security.Response
{
    [XmlRoot("ApplicationGroup")]
    public class ApplicationGroupDTO
    {
        [XmlElement("GroupID")]
        public int GroupID { get; set; }
        [XmlElement("ApplicationID")]
        public int? ApplicationID { get; set; }
        [XmlIgnore]
        public string ApplicationName { get; set; }
        [XmlElement("GroupName")]
        public string GroupName { get; set; }
        [XmlElement("Description")]
        public string Description { get; set; }
        [XmlElement("Type")]
        public string Type { get; set; }
        [XmlElement("Sequence")]
        public int? Sequence { get; set; }
        [XmlElement("Status")]
        public string Status { get; set; }
        [XmlElement("CreatedBy")]
        public int? CreatedBy { get; set; }
        [XmlIgnore]
        public DateTime? CreatedOn { get; set; }
        [XmlElement("ModifiedBy")]
        public int? ModifiedBy { get; set; }
        [XmlIgnore]
        public DateTime? ModifiedOn { get; set; }

        [XmlArray("GroupNavigations")]
        [XmlArrayItem("Items")]
        public List<GroupNavigationDTO> GroupNavigations { get; set; }
    }
}
