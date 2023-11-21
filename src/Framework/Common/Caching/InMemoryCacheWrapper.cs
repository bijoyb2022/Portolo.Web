using System;

namespace Portolo.Framework.Common.Caching
{
    public sealed class InMemoryCacheWrapper : CacheWrapper
    {
        private readonly ICacheProvider internalCache;

        public InMemoryCacheWrapper(ICacheProvider cache)
        {
            this.internalCache = cache;
        }

        protected override void DeleteValue(string key)
        {
            this.internalCache.Invalidate(key);
        }

        protected override void DeleteValueByPattern(string prefixKey)
        {
            this.internalCache.InvalidateByPattern(prefixKey);
        }

        protected override string GetValue(string key)
        {
            return this.internalCache.Get(key) as string;
        }

        protected override void SetValue(string key, string objectToCache, TimeSpan? expiry = null)
        {
            var expiryInMinutes = expiry.HasValue ? Convert.ToInt32(expiry.Value.TotalMinutes) : 62; // 5 minute default?
            this.internalCache.Set(key, objectToCache, expiryInMinutes);
        }
    }
}