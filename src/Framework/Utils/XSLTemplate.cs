using System.Collections;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Xsl;
using Portolo.Email;

namespace Portolo.Framework.Utils
{
    public static class XSLT
    {
        public static string Execute(string xslttemplatename, IDictionary dictionary = null)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            var xslt = new XslTransform();
#pragma warning restore CS0618 // Type or member is obsolete
            xslt.Load(xslttemplatename);
            var xmldoc = new XmlDocument();
            xmldoc.AppendChild(xmldoc.CreateElement("DocumentRoot"));
            var xpathnav = xmldoc.CreateNavigator();
            var xslarg = new XsltArgumentList();
            if (dictionary != null)
            {
                foreach (DictionaryEntry entry in dictionary)
                {
                    xslarg.AddExtensionObject(entry.Key.ToString(), entry.Value);
                }
            }

            var stringBuilder = new StringBuilder();
            var xmlwriter = new XmlTextWriter(new StringWriter(stringBuilder));
            xslt.Transform(xpathnav, xslarg, xmlwriter, null);
            return stringBuilder.ToString();
        }

        public static async Task ExecuteMailTemplateAsync(IEmailSender emailSender,
                                                           string from,
                                                           string to,
                                                           string xslttemplatename,
                                                           IDictionary param = null,
                                                           IDictionary extensionObject = null)
        {
            string subjecText, bodyText;
#pragma warning disable CS0618 // Type or member is obsolete
            var xslt = new XslTransform();
#pragma warning restore CS0618 // Type or member is obsolete
            xslt.Load(xslttemplatename);
            var xmldoc = new XmlDocument();
            xmldoc.AppendChild(xmldoc.CreateElement("DocumentRoot"));
            var xpathnav = xmldoc.CreateNavigator();
            var xslarg = new XsltArgumentList();
            if (extensionObject != null)
            {
                foreach (DictionaryEntry entry in extensionObject)
                {
                    xslarg.AddExtensionObject(entry.Key.ToString(), entry.Value);
                }
            }

            if (param != null)
            {
                foreach (DictionaryEntry entry in param)
                {
                    xslarg.AddParam(entry.Key.ToString(), string.Empty, entry.Value);
                }
            }

            var stringBuilder = new StringBuilder();
            var xmlwriter = new XmlTextWriter(new StringWriter(stringBuilder));
            xslt.Transform(xpathnav, xslarg, xmlwriter, null);

            var xemailDoc = new XmlDocument();
            xemailDoc.LoadXml(stringBuilder.ToString());
            var titlenode = xemailDoc.SelectSingleNode("//title");
            subjecText = titlenode.InnerText;
            var bodynode = xemailDoc.SelectSingleNode("//body");
            bodyText = bodynode.InnerXml;
            if (bodyText.Length > 0)
            {
                bodyText = bodyText.Replace("&amp;", "&");
            }

            await emailSender.SendHtmlEmailAsync(subjecText, bodyText, from, to, Fallback.None).ConfigureAwait(false);
        }
    }
}