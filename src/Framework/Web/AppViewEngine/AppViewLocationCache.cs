using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace Portolo.Framework.Web
{
    public class AppViewLocationCache : IViewLocationCache
    {
        private static readonly object Key = new object();
        private readonly IViewLocationCache cache;

        public AppViewLocationCache(IViewLocationCache cache)
        {
            this.cache = cache;
        }

        public string GetViewLocation(HttpContextBase httpContext, string key)
        {
            var d = GetRequestCache(httpContext);
            string location;
            if (!d.TryGetValue(key, out location))
            {
                location = this.cache.GetViewLocation(httpContext, key);
                d[key] = location;
            }

            return location;
        }

        public void InsertViewLocation(HttpContextBase httpContext, string key, string virtualPath)
        {
            this.cache.InsertViewLocation(httpContext, key, virtualPath);
        }

        private static IDictionary<string, string> GetRequestCache(HttpContextBase httpContext)
        {
            var d = httpContext.Items[Key] as IDictionary<string, string>;
            if (d == null)
            {
                d = new Dictionary<string, string>();
                httpContext.Items[Key] = d;
            }

            return d;
        }
    }
}