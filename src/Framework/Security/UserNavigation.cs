using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Portolo.Framework.Security
{
    [XmlRoot("Item")]
    [DebuggerDisplay("{NavigationPageName} {NavigationId}")]
    public class UserNavigation
    {
        [XmlElement("ItemId")]
        public int NavigationId { get; set; }

        [XmlElement("ParentItemId")]
        public int ParentNavigationId { get; set; }

        [XmlElement("ItemText")]
        public string NavigationText { get; set; }

        [XmlElement("ItemPageName")]
        public string NavigationPageName { get; set; }

        [XmlElement("ItemCommand")]
        public string NavigationCommand { get; set; }

        [XmlElement("ItemIcon")]
        public string NavigationIcon { get; set; }

        [XmlElement("Item")]
        public List<UserNavigation> ChildNavigation { get; set; }
    }
}