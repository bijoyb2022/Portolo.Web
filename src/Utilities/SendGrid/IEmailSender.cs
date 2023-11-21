using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portolo.Email
{
    public interface IEmailSender
    {
        Task SendHtmlEmailAsync(string subject, string body, string from, string to, Fallback fallbackAddress = Fallback.Default, IEnumerable<string> attachments = null);
        Task SendHtmlEmailAsync(string subject, string body, string from, IEnumerable<string> to, Dictionary<string, string> customArgs, Fallback fallbackAddress = Fallback.Default, IEnumerable<string> attachments = null);
        Task SendHtmlEmailWithInMemoryAttachmentsAsync(string subject, string body, string from, string to, Fallback fallbackAddress = Fallback.Default, IEnumerable<Tuple<string, byte[]>> attachments = null);
        Task SendHtmlEmailWithInMemoryAttachmentsAsync(string subject, string body, string from, IEnumerable<string> to, Dictionary<string, string> customArgs, Fallback fallbackAddress = Fallback.Default, IEnumerable<Tuple<string, byte[]>> attachments = null);
        Task SendHtmlEmailAsync(string subject, string body, string from, IEnumerable<string> to, Fallback fallbackAddress = Fallback.Default, IEnumerable<string> attachments = null);
        Task SendHtmlEmailWithInMemoryAttachmentsAsync(string subject, string body, string from, IEnumerable<string> to, Fallback fallbackAddress = Fallback.Default, IEnumerable<Tuple<string, byte[]>> attachments = null);
        Task SendHtmlEmailWithInMemoryAttachmentsMessageCenterAsync(string subject, string body, string from, IEnumerable<string> to, Fallback fallbackAddress = Fallback.Default, IEnumerable<Tuple<string, byte[]>> attachments = null);
    }
}
