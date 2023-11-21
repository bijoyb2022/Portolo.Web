using System;
using System.Collections.Generic;
using System.Linq;

namespace Portolo.Framework.Web
{
    public class Config
    {
        public int CompanyId { get; set; }

        public string ConfigKey { get; set; }

        public object ConfigValue { get; set; }

        public string ConfigDescription { get; set; }
    }
}