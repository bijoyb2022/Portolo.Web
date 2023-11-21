using System;
using System.Collections.Generic;
using System.Linq;

namespace Portolo.Framework.Web
{
    public class UserAttribute
    {
        public int CompanyId { get; set; }

        public string AttributeCode { get; set; }

        public object AttributeValue { get; set; }

        public string AttributeName { get; set; }
    }
}