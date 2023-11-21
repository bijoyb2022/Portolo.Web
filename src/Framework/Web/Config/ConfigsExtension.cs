using System;
using System.Collections.Generic;
using System.Linq;

namespace Portolo.Framework.Web
{
    public static class ConfigsExtension
    {
        public static T ConfigValue<T>(this Configs source, Func<Config, bool> predicate)
        {
            var value = source != null ? source.Where(predicate).Select(c => c.ConfigValue).FirstOrDefault() : default(T);
            return (T)Convert.ChangeType(value, typeof(T));
        }

        public static string ConfigDescription(this Configs source, Func<Config, bool> predicate)
        {
            return source != null ? source.Where(predicate).Select(c => c.ConfigDescription).FirstOrDefault() : default;
        }
    }
}