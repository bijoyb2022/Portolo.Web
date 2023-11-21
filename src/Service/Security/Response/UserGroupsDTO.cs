using System.Xml.Serialization;

namespace Portolo.Security.Response
{
    public class UserGroupsDTO
    {
        [XmlAttribute("CompanyID")]
        public int CompanyID { get; set; }

        [XmlAttribute("OwnerID")]
        public int OwnerID { get; set; }

        [XmlAttribute("GroupId")]
        public int GroupId { get; set; }

        public string GroupName { get; set; }

        public int ApplicationId { get; set; }

        public string ApplicationName { get; set; }

        [XmlAttribute("HasAccess")]
        public int HasAccess { get; set; }
    }
}
