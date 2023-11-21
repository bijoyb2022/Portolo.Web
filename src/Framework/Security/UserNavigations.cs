using System.Collections.Generic;
using System.Xml.Serialization;

namespace Portolo.Framework.Security
{
    [XmlRoot("Navigation")]
    public class UserNavigations
    {
        [XmlElement("Item")]
        public List<UserNavigation> MenuItems { get; set; } = new List<UserNavigation>();
    }
}