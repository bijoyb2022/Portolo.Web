using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
#if NET472 || NET45
using System.Web.Mvc;
#endif
using System.Xml;
using System.Xml.Serialization;
using AutoMapper;
using Newtonsoft.Json;

namespace Portolo.Framework.Utils
{
    public static class ExtensionMethod
    {
        public static T GetPropertyValue<T>(this object obj, string name)
        {
            var retVal = GetPropertyValue(obj, name);
            if (retVal == null)
            {
                return default;
            }

            return (T)retVal;
        }

        /// <summary>
        /// Get a property value using reflection.
        /// </summary>
        /// <returns>Object.</returns>
        public static object GetPropertyValue(this object obj, string name)
        {
            foreach (var part in name.Split('.'))
            {
                if (obj == null)
                {
                    return null;
                }

                var type = obj.GetType();
                var info = type.GetProperty(part);
                if (info == null)
                {
                    return null;
                }

                obj = info.GetValue(obj, null);
            }

            return obj;
        }

        public static void IgnoreIfSourceIsNull<T, TDestination, TMember>(this IMemberConfigurationExpression<T, TDestination, TMember> expression)
        {
            expression.Condition((src, dest, srcVal) => srcVal != null);
        }

        public static string ToDescription(this Enum value)
        {
            var da = (DescriptionAttribute[])value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return da.Length > 0 ? da[0].Description : value.ToString();
        }

        public static string ToFormatedDescription(this Enum value, params string[] formatWith)
        {
            var da = (DescriptionAttribute[])value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return da.Length > 0 ? string.Format(da[0].Description, formatWith) : string.Format(value.ToString(), formatWith);
        }

        public static string DefaultIfNull(this string source, string defaultValue) => source ?? defaultValue;

        public static string DefaultIfNullOrEmpty(this string source, string defaultValue) =>
            string.IsNullOrWhiteSpace(source) ? defaultValue : source.Trim();

        public static bool IsNullOrEmpty(this string source) => string.IsNullOrEmpty(source);

        public static string IsNull(this string str, string defaultValue)
        {
            if (str == null || str.IsDBNull())
            {
                return defaultValue;
            }

            return str;
        }

        public static string IsNull(this string str)
        {
            if (str == null)
            {
                return string.Empty;
            }

            return str;
        }

        public static int ToInt(this string current)
        {
            int.TryParse(current, out var convertedValue);
            return convertedValue;
        }

        public static int? ToNullableInt(this string current)
        {
            int.TryParse(current, out var convertedValue);
            return convertedValue;
        }

        public static TValue ConvertTo<TValue>(this string text)
        {
            var typeConverter = TypeDescriptor.GetConverter(typeof(TValue));
            if (typeConverter.CanConvertFrom(text.GetType()))
            {
                return (TValue)typeConverter.ConvertFrom(text);
            }

            typeConverter = TypeDescriptor.GetConverter(text.GetType());
            if (typeConverter.CanConvertTo(typeof(TValue)))
            {
                return (TValue)typeConverter.ConvertTo(text, typeof(TValue));
            }

            throw new NotSupportedException();
        }

        public static TU ChangeType<TU>(this object source, TU returnValueIfException)
        {
            try
            {
                return source.ChangeType<TU>();
            }
            catch
            {
                return returnValueIfException;
            }
        }

        public static TU ChangeType<TU>(this object source)
        {
            if (source is TU)
            {
                return (TU)source;
            }

            var destinationType = typeof(TU);
            if (destinationType.IsGenericType && destinationType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                destinationType = new NullableConverter(destinationType).UnderlyingType;
            }

            return (TU)Convert.ChangeType(source, destinationType);
        }

        public static string FormatWith(this string original, params object[] values) => string.Format(original, values);

        public static string CombineWith(this string input, string suffix, string separator = " ")
        {
            if (string.IsNullOrEmpty(input))
            {
                if (string.IsNullOrEmpty(suffix))
                {
                    return string.Empty;
                }

                return suffix;
            }

            if (string.IsNullOrEmpty(suffix))
            {
                return input;
            }

            return string.Format("{0}{1}{2}", input, separator, suffix);
        }

        public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }

        public static void Each<TSource>(this IEnumerable<TSource> elements, Action<TSource> action)
        {
            foreach (var e in elements)
            {
                action(e);
            }
        }

        public static bool Between<TSource>(this TSource value, TSource from, TSource to)
            where TSource : IComparable<TSource>
            =>
            value.CompareTo(from) >= 0 && value.CompareTo(to) <= 0;

        public static bool In<TSource>(this TSource value, params TSource[] list) => list.Contains(value);

        public static IEnumerable<TSource> Distinct<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return source.GroupBy(keySelector).Select(e => e.First());
        }

        public static Dictionary<TFirstKey, Dictionary<TSecondKey, TValue>> Pivot<TSource, TFirstKey, TSecondKey, TValue>(
            this IEnumerable<TSource> source,
            Func<TSource, TFirstKey> firstKeySelector,
            Func<TSource, TSecondKey> secondKeySelector,
            Func<IEnumerable<TSource>, TValue> aggregate)
        {
            var retVal = new Dictionary<TFirstKey, Dictionary<TSecondKey, TValue>>();

            var l = source.ToLookup(firstKeySelector);
            foreach (var item in l)
            {
                var dict = new Dictionary<TSecondKey, TValue>();
                retVal.Add(item.Key, dict);
                var subdict = item.ToLookup(secondKeySelector);
                foreach (var subitem in subdict)
                {
                    dict.Add(subitem.Key, aggregate(subitem));
                }
            }

            return retVal;
        }

        public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource, bool> predicate)
        {
            if (condition)
            {
                return source.Where(predicate);
            }

            return source;
        }

        public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource, int, bool> predicate)
        {
            if (condition)
            {
                return source.Where(predicate);
            }

            return source;
        }

        public static TInner IfNotNull<T, TInner>(this T source, Func<T, TInner> selector)
            where T : class
            =>
            source != null ? selector(source) : default;

        public static bool IsNotNullOrEmpty<TSource>(this IEnumerable<TSource> source)
            where TSource : class
            => source != null && source.Any();

#if NET472 || NET45
        public static IEnumerable<SelectListItem> ToSelectList<T>(this IEnumerable<T> items,
                                                                  Func<T, string> text,
                                                                  Func<T, string> value = null,
                                                                  Func<T, bool> selected = null)
        {
            return items.Select(p => new SelectListItem
            {
                Text = text.Invoke(p),
                Value = value == null ? text.Invoke(p) : value.Invoke(p),
                Selected = selected == null ? false : selected.Invoke(p)
            });
        }
#endif

        public static string ToJson(this object source) => JsonConvert.SerializeObject(source);

        public static T FromJson<T>(this object source) => JsonConvert.DeserializeObject<T>(source as string);

        public static IQueryable<TSource> Page<TSource>(this IQueryable<TSource> source, int page, int pageSize) =>
            source.Skip((page - 1) * pageSize).Take(pageSize);

        public static IEnumerable<TSource> Page<TSource>(this IEnumerable<TSource> source, int page, int pageSize) =>
            source.Skip((page - 1) * pageSize).Take(pageSize);

        public static string ToXml(this object source)
        {
            var ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);

            var serializer = new XmlSerializer(source.GetType());

            using (var textWriter = new StringWriter())
            {
                using (var xmlWriter = XmlWriter.Create(textWriter, new XmlWriterSettings
                {
                    Encoding = new UnicodeEncoding(false, false),
                    Indent = false,
                    OmitXmlDeclaration = true
                }))
                {
                    serializer.Serialize(xmlWriter, source, ns);
                }

                return textWriter.ToString();
            }
        }

        public static string ToXml<T>(this object source)
        {
            string str;
            XmlSerializerNamespaces xmlSerializerNamespace = new XmlSerializerNamespaces();
            xmlSerializerNamespace.Add(string.Empty, string.Empty);
            XmlSerializer xmlSerializer = new XmlSerializer(source.GetType());
            XmlWriterSettings xmlWriterSetting = new XmlWriterSettings()
            {
                Encoding = new UnicodeEncoding(false, false),
                Indent = false,
                OmitXmlDeclaration = true
            };
            using (StringWriter stringWriter = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, xmlWriterSetting))
                {
                    xmlSerializer.Serialize(xmlWriter, source, xmlSerializerNamespace);
                }

                str = stringWriter.ToString();
            }

            return str;
        }

        public static void Merge<TKey, TValue>(this IDictionary<TKey, TValue> first, IDictionary<TKey, TValue> second)
        {
            if (second == null || first == null)
            {
                return;
            }

            foreach (var item in second)
            {
                if (!first.ContainsKey(item.Key))
                {
                    first.Add(item.Key, item.Value);
                }
            }
        }

        public static List<T> ToListof<T>(this DataTable dt)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
            var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToList();
            var objectProperties = typeof(T).GetProperties(flags);
            var targetList = dt.AsEnumerable()
                                   .Select(dataRow =>
                                   {
                                       var instanceOfT = Activator.CreateInstance<T>();

                                       foreach (var properties in objectProperties.Where(
                                           properties => columnNames.Contains(properties.Name) && dataRow[properties.Name] != DBNull.Value))
                                       {
                                           properties.SetValue(instanceOfT, dataRow[properties.Name], null);
                                       }

                                       return instanceOfT;
                                   })
                                   .ToList();

            return targetList;
        }

        private static bool IsDBNull(this string str)
        {
            object obj = str;
            if (obj == DBNull.Value || obj == null)
            {
                return true;
            }

            return false;
        }
    }
}