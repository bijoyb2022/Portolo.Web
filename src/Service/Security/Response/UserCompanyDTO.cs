using System.Collections.Generic;
using System.Xml.Serialization;

namespace Portolo.Security.Response
{
    public class UserCompanyDTO
    {
        [XmlIgnore]
        public int Id { get; set; }

        [XmlAttribute("HasAccess")]
        public int HasAccess { get; set; }

        [XmlAttribute("CompanyID")]
        public int CompanyID { get; set; }

        public string CompanyName { get; set; }

        [XmlIgnore]
        public int? OwnerID { get; set; }

        [XmlAttribute("OwnerID")]
        public string OwnerIDValue
        {
            get => this.OwnerID.HasValue ? this.OwnerID.ToString() : null;
            set => this.OwnerID = !string.IsNullOrEmpty(value) ? int.Parse(value) : default(int?);
        }

        [XmlArray("Groups")]
        [XmlArrayItem("Item")]
        public List<UserGroupsDTO> Groups { get; set; }
    }
}
