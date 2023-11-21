using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using SendGrid.Helpers.Mail;

namespace Portolo.Email
{
    public class EmailSender : BaseEmailSender
    {
        public EmailSender(IConfiguration config)
            : base(config)
        {
        }
        internal override List<EmailAddress> GenerateToAddresses(IEnumerable<string> to, Fallback fallbackAddress = Fallback.Default)
        {
            return to.Select(t => new EmailAddress(t)).ToList();
        }
    }
}
