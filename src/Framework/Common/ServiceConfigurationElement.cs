using System.Collections.Generic;
using System.Configuration;

namespace Portolo.Framework.Common
{
    public class ServiceConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get => (string)this["name"];
            set => this["name"] = value;
        }

        [ConfigurationProperty("contract", IsRequired = true)]
        public string Contract
        {
            get => (string)this["contract"];
            set => this["contract"] = value;
        }

        [ConfigurationProperty("endpointaddress", IsRequired = true)]
        public string EndPointAddress
        {
            get => (string)this["endpointaddress"];
            set => this["endpointaddress"] = value;
        }

        [ConfigurationProperty("dnsidentifier")]
        public string DnsIdentifier
        {
            get => (string)this["dnsidentifier"];
            set => this["dnsidentifier"] = value;
        }

        [ConfigurationProperty("defaultemail")]
        public string DefaultEmail
        {
            get => (string)this["defaultemail"];
            set => this["defaultemail"] = value;
        }

        [ConfigurationProperty("defaultpassword")]
        public string DefaultPassword
        {
            get => (string)this["defaultpassword"];
            set => this["defaultpassword"] = value;
        }

        [ConfigurationProperty("servicemoduleid")]
        public int ServiceModuleId
        {
            get => (int)this["servicemoduleid"];
            set => this["servicemoduleid"] = value;
        }

        [ConfigurationProperty("singleinstance", DefaultValue = true)]
        public bool SingleInstance
        {
            get => (bool)this["singleinstance"];
            set => this["singleinstance"] = value;
        }

        [ConfigurationProperty("enablebinding", DefaultValue = Bindings.wsHttpBinding)]
        public Bindings EnableBinding
        {
            get => (Bindings)this["enablebinding"];
            set => this["enablebinding"] = value;
        }

        [ConfigurationProperty("defaultdbconnection")]
        public string DefaultDbConnection
        {
            get => (string)this["defaultdbconnection"];
            set => this["defaultdbconnection"] = value;
        }

        [ConfigurationProperty("applicationlogin", DefaultValue = false)]
        public bool ApplicationLogin
        {
            get => (bool)this["applicationlogin"];
            set => this["applicationlogin"] = value;
        }
    }
}