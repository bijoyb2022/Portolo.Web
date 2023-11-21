using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Portolo.Security.Response
{
    public class GroupNavigationDTO
    {
        [XmlIgnore]
        public int GroupNavigationID { get; set; }

        [XmlIgnore]
        public int GroupID { get; set; }

        [XmlAttribute("NavigationID")]
        public int NavigationID { get; set; }

        [XmlIgnore]
        public int? CreatedBy { get; set; }

        [XmlIgnore]
        public DateTime? CreatedOn { get; set; }

        [XmlIgnore]
        public int? ModifiedBy { get; set; }

        [XmlIgnore]
        public DateTime? ModifiedOn { get; set; }

        [XmlArray("GroupNavigationItems")]
        [XmlArrayItem("Item")]
        public List<GroupNavigationItemDTO> GroupNavigationItems { get; set; }
    }
}
