using System.Xml.Serialization;

namespace Portolo.Security.Response
{
    public class OwnersDTO
    {
        [XmlAttribute("OwnerId")]
        public int OwnerId { get; set; }

        public string OwnerName { get; set; }
        public string OwnerShortName { get; set; }
        public string OwnerAccountId { get; set; }
        public string Description { get; set; }
    }
}
