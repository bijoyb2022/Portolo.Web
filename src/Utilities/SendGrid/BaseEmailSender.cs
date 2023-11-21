using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Configuration;
using Portolo.Utility.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Portolo.Email
{
    public abstract class BaseEmailSender : IEmailSender
    {
        internal readonly EmailConfigurationSection EmailConfigurationSection;

        protected BaseEmailSender(IConfiguration config)
        {
            this.EmailConfigurationSection = config.GetTypedSection<EmailConfigurationSection>();
        }

        public Task SendHtmlEmailAsync(string subject, string body, string from, string to, Fallback fallbackAddress = Fallback.Default, IEnumerable<string> attachments = null)
        {
            return this.SendHtmlEmailAsync(subject, body, from, new[] { to }, fallbackAddress, attachments);
        }

        public Task SendHtmlEmailWithInMemoryAttachmentsAsync(string subject, string body, string from, string to, Fallback fallbackAddress = Fallback.Default, IEnumerable<Tuple<string, byte[]>> attachments = null)
        {
            return this.SendHtmlEmailWithInMemoryAttachmentsAsync(subject, body, from, new[] { to }, fallbackAddress, attachments);
        }

        public async Task SendHtmlEmailAsync(string subject, string body, string from, IEnumerable<string> to, Fallback fallbackAddress = Fallback.Default, IEnumerable<string> attachments = null)
        {
            var msg = this.CreateMessage(subject, body, from, to, fallbackAddress);

            await AddAttachmentsToMessageAsync(msg, attachments).ConfigureAwait(false);

            await this.SendEmailAsync(msg).ConfigureAwait(false);
        }

        public async Task SendHtmlEmailAsync(string subject, string body, string from, IEnumerable<string> to, Dictionary<string, string> customArgs, Fallback fallbackAddress = Fallback.Default,  IEnumerable<string> attachments = null)
        {
            var msg = this.CreateMessage(subject, body, from, to, customArgs, fallbackAddress);

            await AddAttachmentsToMessageAsync(msg, attachments).ConfigureAwait(false);

            await this.SendEmailAsync(msg).ConfigureAwait(false);
        }

        public async Task SendHtmlEmailWithInMemoryAttachmentsAsync(string subject, string body, string from, IEnumerable<string> to, Fallback fallbackAddress = Fallback.Default, IEnumerable<Tuple<string, byte[]>> attachments = null)
        {
            try
            {
                var message = this.CreateMessage(subject, body, from, to, fallbackAddress);

                if (attachments != null)
                {
                    foreach (var attachment in attachments)
                    {
                        if (attachment != null && !string.IsNullOrEmpty(attachment.Item1) && attachment.Item2 != null)
                        {
                            message.AddAttachment(attachment.Item1, Convert.ToBase64String(attachment.Item2));
                        }
                        else
                        {
                            string attachmentName = attachment != null && !string.IsNullOrEmpty(attachment.Item1) ?
                                attachment.Item1 :
                                "No attachment name specified";

                            string toAddresses = GenerateToAddressList(message);

                            var telemetryClient = new TelemetryClient();
                            telemetryClient.TrackEvent(
                                "InvalidEmailAttachment",
                                new Dictionary<string, string>
                                {
                                { "From", message.From.Email },
                                { "To", toAddresses },
                                { "Subject", message.Subject },
                                { "Body", message.HtmlContent },
                                { "Attachment", attachmentName }
                                });
                        }
                    }
                }

                await this.SendEmailAsync(message).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message.ToString());
            }
        }

        public async Task SendHtmlEmailWithInMemoryAttachmentsAsync(string subject, string body, string from, IEnumerable<string> to, Dictionary<string, string> customArgs, Fallback fallbackAddress = Fallback.Default,  IEnumerable<Tuple<string, byte[]>> attachments = null)
        {
            var message = this.CreateMessage(subject, body, from, to, customArgs, fallbackAddress);

            if (attachments != null)
            {
                foreach (var attachment in attachments)
                {
                    if (attachment != null && !string.IsNullOrEmpty(attachment.Item1) && attachment.Item2 != null)
                    {
                        message.AddAttachment(attachment.Item1, Convert.ToBase64String(attachment.Item2));
                    }
                    else
                    {
                        string attachmentName = attachment != null && !string.IsNullOrEmpty(attachment.Item1) ?
                            attachment.Item1 :
                            "No attachment name specified";

                        string toAddresses = GenerateToAddressList(message);

                        var telemetryClient = new TelemetryClient();
                        telemetryClient.TrackEvent(
                            "InvalidEmailAttachment",
                            new Dictionary<string, string>
                            {
                                { "From", message.From.Email },
                                { "To", toAddresses },
                                { "Subject", message.Subject },
                                { "Body", message.HtmlContent },
                                { "Attachment", attachmentName }
                            });
                    }
                }
            }

            await this.SendEmailAsync(message).ConfigureAwait(false);
        }

        internal static async Task AddAttachmentsToMessageAsync(SendGridMessage message, IEnumerable<string> attachments)
        {
            if (attachments == null)
            {
                return;
            }

            foreach (var attachment in attachments)
            {
                byte[] bytes = null;
                string fileName;

                var fileNameIndex = attachment.LastIndexOf("/");

                if (fileNameIndex == -1)
                {
                    fileName = attachment.Substring(attachment.LastIndexOf(@"\") + 1);
                    bytes = File.ReadAllBytes(attachment);
                }
                else
                {
                    fileName = attachment.Substring(fileNameIndex + 1, attachment.IndexOf('?', fileNameIndex) - fileNameIndex - 1);
                    using (var webclient = new WebClient())
                    {
                        bytes = await webclient.DownloadDataTaskAsync(new Uri(attachment)).ConfigureAwait(false);
                    }
                }

                if (bytes == null)
                {
                    string toAddresses = GenerateToAddressList(message);

                    var telemetryClient = new TelemetryClient();
                    telemetryClient.TrackEvent(
                        "InvalidEmailAttachment",
                        new Dictionary<string, string>
                        {
                            { "From", message.From.Email },
                            { "To", toAddresses },
                            { "Subject", message.Subject },
                            { "Body", message.HtmlContent },
                            { "Attachment", attachment }
                        });
                }
                else
                {
                    var file = Convert.ToBase64String(bytes);
                    message.AddAttachment(fileName, file);
                }
            }
        }

        internal abstract List<EmailAddress> GenerateToAddresses(IEnumerable<string> to, Fallback fallbackAddress = Fallback.Default);

        private static string GenerateToAddressList(SendGridMessage message)
        {
            string toAddresses = string.Empty;

            for (int i = 0; i < message.Personalizations.Count; i++)
            {
                toAddresses += $"{i}: {string.Join(",", message.Personalizations[i].Tos.Select(t => t.Email))}\n";
            }

            if (string.IsNullOrEmpty(toAddresses))
            {
                toAddresses = "No to email address set";
            }

            return toAddresses;
        }

        private SendGridMessage CreateMessage(string subject, string body, string from, IEnumerable<string> to, Fallback fallbackAddress = Fallback.Default)
        {
            var message = new SendGridMessage
            {
                From = new EmailAddress(from),
                Subject = subject,
                HtmlContent = string.IsNullOrEmpty(body) ? " " : body
            };

            var fullToList = to.SelectMany(t => t.Split(',')).Select(s => s.Trim()).Where(r => !string.IsNullOrEmpty(r)).Distinct();
            message.AddTos(this.GenerateToAddresses(fullToList, fallbackAddress));

            return message;
        }

        private SendGridMessage CreateMessage(string subject, string body, string from, IEnumerable<string> to, Dictionary<string, string> customArgs, Fallback fallbackAddress = Fallback.Default)
        {
            var message = new SendGridMessage
            {
                From = new EmailAddress(from),
                Subject = subject,
                HtmlContent = string.IsNullOrEmpty(body) ? " " : body
            };

            message.CustomArgs = customArgs;
            var fullToList = to.SelectMany(t => t.Split(',')).Select(s => s.Trim()).Where(r => !string.IsNullOrEmpty(r)).Distinct();
            message.AddTos(this.GenerateToAddresses(fullToList, fallbackAddress));

            return message;
        }

        private async Task SendEmailAsync(SendGridMessage message)
        {
            try
            {
                var telemetryClient = new TelemetryClient();
                string toAddresses = GenerateToAddressList(message);

                telemetryClient.TrackEvent(
                    "SendGrid",
                    new Dictionary<string, string>
                    {
                    { "From", message.From.Email },
                    { "To", toAddresses },
                    { "Subject", message.Subject },
                    { "StackTrace", Environment.StackTrace }
                    });

                var client = new SendGridClient(this.EmailConfigurationSection.ApiKey);
                var response = await client.SendEmailAsync(message).ConfigureAwait(false);

                if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Accepted)
                {
                    var result = await response.Body.ReadAsStringAsync().ConfigureAwait(false);

                    telemetryClient.TrackEvent(
                        "SendGrid",
                        new Dictionary<string, string>
                        {
                        { "Result", result },
                        { "From", message.From.Email },
                        { "To", toAddresses },
                        { "Subject", message.Subject },
                        { "Body", message.HtmlContent }
                        });
                }
            }
            catch(Exception ex)
            {
                throw new ApplicationException(ex.Message.ToString());
            }
        }
        public Task SendHtmlEmailWithInMemoryAttachmentsMessageCenterAsync(string subject, string body, string from, IEnumerable<string> to, Fallback fallbackAddress = Fallback.Default, IEnumerable<Tuple<string, byte[]>> attachments = null)
        {
            return this.SendHtmlEmailWithInMemoryAttachmentsAsync(subject, body, from, to, fallbackAddress, attachments);
        }
    }
}