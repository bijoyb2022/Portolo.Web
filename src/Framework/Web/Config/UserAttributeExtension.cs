using System;
using System.Collections.Generic;
using System.Linq;

namespace Portolo.Framework.Web
{
    public static class UserAttributeExtension
    {
        public static T Value<T>(this UserAttributes source, Func<UserAttribute, bool> predicate)
        {
            var value = source != null ? source.Where(predicate).Select(c => c.AttributeValue).FirstOrDefault() : default(T);
            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}