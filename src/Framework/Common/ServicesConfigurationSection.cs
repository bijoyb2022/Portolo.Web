using System.Collections.Generic;
using System.Configuration;

namespace Portolo.Framework.Common
{
    public class ServicesConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("services", IsRequired = true)]
        [ConfigurationCollection(typeof(ServiceConfigurationElement), AddItemName = "service")]
        public GenericConfigurationElementCollection<ServiceConfigurationElement> Services =>
            (GenericConfigurationElementCollection<ServiceConfigurationElement>)this["services"];
    }
}