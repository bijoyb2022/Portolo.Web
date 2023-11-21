using System;

namespace Portolo.Email
{
    public class EmailTemplate
    {
        public string Name { get; set; }
        public Func<object, string> Subject { get; set; }
        public Func<object, string> BodyHtml { get; set; }
        public Func<object, string> BodyText { get; set; }
    }
}
