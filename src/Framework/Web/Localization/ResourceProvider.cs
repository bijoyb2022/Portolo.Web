using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Portolo.Framework.Common.Caching;
using Portolo.Framework.Utils;

namespace Portolo.Framework.Web.Localization
{
    public class ResourceProvider : IResourceProvider
    {
        // Cache list of resources
        private static readonly object LockResources = new object();
        private static Dictionary<string, ResourceEntry> resources;

        public ResourceProvider()
        {
            // TODO: TASK-80 investigate requirement of cache on localization resources
            this.Cache = false; // By default, enable caching for performance
        }

        protected bool Cache { get; set; } // Cache resources

        /// <summary>
        /// Returns a single resource for a specific culture.
        /// </summary>
        /// <param name="name">Resorce name (ie key).</param>
        /// <param name="culture">Culture code.</param>
        /// <returns>Resource.</returns>
        public object GetResource(string name, string culture)
        {
            ResourceEntry resourceEntry;
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Resource name cannot be null or empty.");
            }

            if (string.IsNullOrWhiteSpace(culture))
            {
                throw new ArgumentException("Culture name cannot be null or empty.");
            }

            culture = culture.ToLowerInvariant();
            if (this.Cache && resources == null)
            {
                lock (LockResources)
                {
                    if (resources == null)
                    {
                        resources = this.ReadResources().ToDictionary(r => string.Format("{0}", r.Key));
                    }
                }
            }

            if (this.Cache)
            {
                resourceEntry = resources[string.Format("{0}", name)];
                return resourceEntry.Value;
            }

            resourceEntry = this.ReadResource(name, culture);
            return resourceEntry.Value;
        }

        /// <summary>
        /// Returns all resources for all cultures. (Needed for caching).
        /// </summary>
        /// <returns>A list of resources.</returns>
        protected List<ResourceEntry> ReadResources()
        {
            var cacheProvider = SingletonCacheProvider.GetInstance;

            var catchKey = string.Format("Global_Resources_{0}", CultureInfo.CurrentCulture.Name);
            return cacheProvider.Get(catchKey) as List<ResourceEntry>;
        }

        /// <summary>
        /// Returns a single resource for a specific culture.
        /// </summary>
        /// <param name="name">Resorce name (ie key).</param>
        /// <param name="culture">Culture code.</param>
        /// <returns>Resource.</returns>
        protected ResourceEntry ReadResource(string name, string culture)
        {
            var resourceEntry = new ResourceEntry();
            var cacheProvider = SingletonCacheProvider.GetInstance;

            var catchKey = string.Format("Global_Resources_{0}", culture);
            IList<ResourceEntry> resources = cacheProvider.Get(catchKey) as List<ResourceEntry>;
            if (resources.IsNotNullOrEmpty())
            {
                resourceEntry = resources.Where(r => r.Key == name).IfNotNull(r => r.FirstOrDefault());
            }

            if (resourceEntry?.Value.IsNullOrEmpty() != false)
            {
                catchKey = string.Format("Global_Resources_{0}", "en-us");
                resources = cacheProvider.Get(catchKey) as List<ResourceEntry>;
                resourceEntry = resources.Where(r => r.Key == name).IfNotNull(r => r.FirstOrDefault());
            }

            return resourceEntry;
        }
    }
}