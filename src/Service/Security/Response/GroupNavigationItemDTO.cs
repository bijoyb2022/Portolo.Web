using System;
using System.Xml.Serialization;

namespace Portolo.Security.Response
{
    public class GroupNavigationItemDTO
    {
        [XmlIgnore]
        public int GroupNavigationItemID { get; set; }

        [XmlIgnore]
        public int GroupID { get; set; }

        [XmlAttribute("NavigationItemID")]
        public int NavigationItemID { get; set; }

        [XmlIgnore]
        public int ApplicationID { get; set; }

        [XmlIgnore]
        public int? CreatedBy { get; set; }

        [XmlIgnore]
        public DateTime? CreatedOn { get; set; }

        [XmlIgnore]
        public int? ModifiedBy { get; set; }

        [XmlIgnore]
        public DateTime? ModifiedOn { get; set; }
    }
}
