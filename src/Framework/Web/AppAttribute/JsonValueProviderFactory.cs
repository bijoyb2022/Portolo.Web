using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Portolo.Framework.Web
{
    public sealed class UltimateJsonValueProviderFactory : ValueProviderFactory
    {
        public override IValueProvider GetValueProvider(ControllerContext controllerContext)
        {
            if (controllerContext == null)
            {
                throw new ArgumentNullException("controllerContext");
            }

            if (!controllerContext.HttpContext.Request.ContentType.StartsWith("application/json", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            var streamReader = new StreamReader(controllerContext.HttpContext.Request.InputStream);
            var jSONReader = new JsonTextReader(streamReader);
            if (!jSONReader.Read())
            {
                return null;
            }

            var jSONSerializer = new JsonSerializer();
            jSONSerializer.Converters.Add(new ExpandoObjectConverter());

            object jSONObject;
            if (jSONReader.TokenType == JsonToken.StartArray)
            {
                jSONObject = jSONSerializer.Deserialize<List<ExpandoObject>>(jSONReader);
            }
            else
            {
                jSONObject = jSONSerializer.Deserialize<ExpandoObject>(jSONReader);
            }

            var backingStore = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            AddToBackingStore(backingStore, string.Empty, jSONObject);
            return new DictionaryValueProvider<object>(backingStore, CultureInfo.CurrentCulture);
        }

        private static void AddToBackingStore(Dictionary<string, object> backingStore, string prefix, object value)
        {
            var d = value as IDictionary<string, object>;
            if (d != null)
            {
                foreach (var entry in d)
                {
                    AddToBackingStore(backingStore, MakePropertyKey(prefix, entry.Key), entry.Value);
                }

                return;
            }

            var l = value as IList;
            if (l != null)
            {
                for (var i = 0; i < l.Count; i++)
                {
                    AddToBackingStore(backingStore, MakeArrayKey(prefix, i), l[i]);
                }

                return;
            }

            backingStore[prefix] = value;
        }

        private static string MakeArrayKey(string prefix, int index) => prefix + "[" + index.ToString(CultureInfo.InvariantCulture) + "]";

        private static string MakePropertyKey(string prefix, string propertyName) =>
            string.IsNullOrEmpty(prefix) ? propertyName : prefix + "." + propertyName;
    }
}