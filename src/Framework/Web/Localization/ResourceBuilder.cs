using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Portolo.Framework.Common.Caching;

namespace Portolo.Framework.Web.Localization
{
    public class ResourceBuilder
    {
        /// <summary>
        /// Generates a class with properties for each resource key.
        /// </summary>
        /// <param name="provider">Resource provider instance.</param>
        /// <param name="namespaceName">Name of namespace containing the resource class.</param>
        /// <param name="className">Name of the class.</param>
        /// <param name="filePath">Where to generate the source file.</param>
        /// <param name="summaryCulture">If not null, adds a &lt;summary&gt; tag to each property using the specified culture.</param>
        /// <returns>Generated class file full path.</returns>
        public string Create(ResourceProvider provider,
                             string namespaceName = "nVisionGlobal.Framework.Web.Localization",
                             string className = "Resources",
                             string filePath = null,
                             string summaryCulture = null)
        {
            var resourceEntry = new ResourceEntry();
            var cacheProvider = SingletonCacheProvider.GetInstance;

            var catchKey = string.Format("en-us.{0}", "Resources");
            IList<ResourceEntry> resources = cacheProvider.Get(catchKey) as List<ResourceEntry>;

            var sbKeys = new StringBuilder();

            if (resources == null || resources.Count == 0)
            {
                throw new System.Exception(string.Format("No resources were found in {0}", provider.GetType().Name));
            }

            // Get a unique list of resource names (keys)
            var keys = resources.Select(r => r.Key).Distinct();

            const string header = @"using System;
                using nVisionGlobal.Application.Web;
                using System.Globalization;
    
                namespace {0} {{

                        public partial class {1} {{
                            private static IResourceProvider resourceProvider = new {2}();

                    {3}
                        }}        
                }}"; // {0}: namespace {1}:class name   {2}:provider class name   {3}: body

            const string property = @"
                        {1}
                        public static {2} {0} {{
                               get {{
                                   return resourceProvider.GetResource(""{0}"", AppConfig.CurrentCulture) as {2};
                               }}
                            }}"; // {0}: key

            foreach (var key in keys)
            {
                var resource = resources.Where(r => r.Key == key).FirstOrDefault();
                if (resource == null)
                {
                    throw new System.Exception(string.Format("Could not find resource {0}", key));
                }

                sbKeys.Append(new string(' ', 12)); // indentation
                sbKeys.AppendFormat(property,
                                    key,
                                    summaryCulture == null ? string.Empty : string.Format("/// <summary>{0}</summary>", resource.Key),
                                    resource.Type);
                sbKeys.AppendLine();
            }

            if (filePath == null)
            {
                filePath = Path.Combine(HttpContext.Current.Server.MapPath("~"), "Resources.cs");
            }

            // write to file
            using (var writer = File.CreateText(filePath))
            {
                // write header along with keys
                writer.WriteLine(header, namespaceName, className, provider.GetType().Name, sbKeys);
                writer.Flush();
                writer.Close();
            }

            return filePath;
        }
    }
}