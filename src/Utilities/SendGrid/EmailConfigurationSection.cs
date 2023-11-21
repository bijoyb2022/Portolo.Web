using System.Collections.Generic;
using Portolo.Utility.Configuration;

namespace Portolo.Email
{
    public class EmailConfigurationSection : ConfigurationSection
    {
        public override string SectionName => "Email";
        public string ApiKey { get; set; }
        public string AccountEmailAddress { get; set; }
        public string EmailFrom { get; set; }
        public string ApplicationLink { get; set; }
        public string EmailLink { get; set; }
        public string CancelledEmailto { get; set; }
        public string HelpDeskEmail { get; set; }
        public string NoReplyEmail { get; set; }
        public bool UseFallbackEmailer { get; set; }
        public Dictionary<Fallback, string> FallbackAddresses { get; set; }
    }
}
