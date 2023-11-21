using System.Collections.Generic;
using System.Xml.Serialization;

namespace Portolo.Framework.Security
{
    [XmlRoot("NavigationItems")]
    public class NavigationItems : List<NavigationItem>
    {
    }
}