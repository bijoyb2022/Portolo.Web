using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using SendGrid.Helpers.Mail;

namespace Portolo.Email
{
    public class FallbackEmailSender : BaseEmailSender
    {
        private readonly IConfiguration _config;
        public FallbackEmailSender(IConfiguration config)
            : base(config)
        {
            _config = config;
        }

        internal override List<EmailAddress> GenerateToAddresses(IEnumerable<string> to, Fallback fallbackAddress = Fallback.Default)
        {
            return fallbackAddress == Fallback.None ?
                        to.Select(t => new EmailAddress(t)).ToList() :
                        new List<EmailAddress> { new EmailAddress(_config.GetSection("FallbackAddresses:"+ fallbackAddress).Value) };
        }
    }
}
