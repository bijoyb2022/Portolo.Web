using System.Diagnostics;
using System.Xml.Serialization;

namespace Portolo.Framework.Security
{
    [DebuggerDisplay("{Name}")]
    [XmlRoot("Item")]
    public class NavigationItem
    {
        [XmlElement("ItemText")]
        public string Text { get; set; }

        [XmlElement("ItemName")]
        public string Name { get; set; }

        [XmlElement("ItemCommand")]
        public string Command { get; set; }

        [XmlElement("ItemStatus")]
        public string Status { get; set; }

        [XmlElement("ItemSequence")]
        public int Sequence { get; set; }

        [XmlElement("ItemIcon")]
        public string Icon { get; set; }
    }
}