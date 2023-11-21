using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portolo.Email
{
    public static class IEmailSenderExtensions
    {
        public static Task SendEmailAsync(this IEmailSender sender, EmailTemplate template, object data, string from, string to, Fallback fallback = Fallback.Default, IEnumerable<string> attachments = null)
        {
            return sender.SendEmailAsync(template, data, from, new[] { to }, fallback, attachments);
        }

        public static Task SendEmailWithInMemoryAttachmentsAsync(this IEmailSender sender, EmailTemplate template, object data, string from, string to, Fallback fallback = Fallback.Default, IEnumerable<Tuple<string, byte[]>> attachments = null)
        {
            return sender.SendEmailWithInMemoryAttachmentsAsync(template, data, from, new[] { to }, fallback, attachments);
        }

        public static Task SendEmailAsync(this IEmailSender sender, EmailTemplate template, object data, string from, IEnumerable<string> to, Fallback fallback = Fallback.Default, IEnumerable<string> attachments = null)
        {
            var subject = template.Subject(data);
            var body = template.BodyHtml(data);

            var isPropertyExist = ((IDictionary<string, object>)data).ContainsKey("NotificationId");
            if (isPropertyExist)
            {
                string notificationId = ((IDictionary<string, object>)data)["NotificationId"].ToString();
                string claimKey = ((IDictionary<string, object>)data)["ClaimKey"].ToString();
                string sendersEmail = ((IDictionary<string, object>)data)["SendersEmail"].ToString();
                var customArgs = new Dictionary<string, string>()
                {
                    { "NotificationId", notificationId.ToString() },
                    { "FromEmail", from },
                    { "Module", "Claim" },
                    { "ClaimKey", claimKey.ToString() },
                    { "SendersEmail", sendersEmail.ToString() }
                };
                return sender.SendHtmlEmailAsync(subject, body, from, to, customArgs, fallback, attachments);
            }

            return sender.SendHtmlEmailAsync(subject, body, from, to, fallback, attachments);
        }

        public static Task SendEmailWithInMemoryAttachmentsAsync(this IEmailSender sender, EmailTemplate template, object data, string from, IEnumerable<string> to, Fallback fallback = Fallback.Default, IEnumerable<Tuple<string, byte[]>> attachments = null)
        {
            var subject = template.Subject(data);
            var body = template.BodyHtml(data);

            var isPropertyExist = ((IDictionary<string, object>)data).ContainsKey("NotificationId");
            if (isPropertyExist)
            {
                string notificationId = ((IDictionary<string, object>)data)["NotificationId"].ToString();
                string claimKey = ((IDictionary<string, object>)data)["ClaimKey"].ToString();
                string sendersEmail = ((IDictionary<string, object>)data)["SendersEmail"].ToString();

                var customArgs = new Dictionary<string, string>()
                {
                    { "NotificationId", notificationId.ToString() },
                    { "FromEmail", from },
                    { "Module", "Claim" },
                    { "ClaimKey", claimKey.ToString() },
                    { "SendersEmail", sendersEmail.ToString() }
                };
                return sender.SendHtmlEmailWithInMemoryAttachmentsAsync(subject, body, from, to, customArgs, fallback, attachments);
            }

            return sender.SendHtmlEmailWithInMemoryAttachmentsAsync(subject, body, from, to, fallback, attachments);
        }

        public static async Task SendIndividualEmailsAsync(this IEmailSender sender, EmailTemplate template, object data, string from, IEnumerable<string> to, Fallback fallback = Fallback.Default, IEnumerable<string> attachments = null)
        {
            foreach (var email in to.Where(email => !string.IsNullOrWhiteSpace(email)))
            {
                await sender.SendEmailAsync(template, data, from, new[] { email }, fallback, attachments).ConfigureAwait(false);
            }
        }

        public static async Task SendIndividualEmailsWithInMemoryAttachmentsAsync(this IEmailSender sender, EmailTemplate template, object data, string from, IEnumerable<string> to, Fallback fallback = Fallback.Default, IEnumerable<Tuple<string, byte[]>> attachments = null)
        {
            foreach (var email in to.Where(email => !string.IsNullOrWhiteSpace(email)))
            {
                await sender.SendEmailWithInMemoryAttachmentsAsync(template, data, from, new[] { email }, fallback, attachments).ConfigureAwait(false);
            }
        }
    }
}
