using System;
using System.Linq;
using System.Runtime.Caching;

namespace Portolo.Framework.Common.Caching
{
    public class MemoryCacheProvider : ICacheProvider
    {
        private ObjectCache Cache => MemoryCache.Default;

        public static CacheItemPolicy GetPolicy(int expiryInMinutes, Action<CacheEntryUpdateArguments> method)
        {
            var result = new TimeSpan(0, expiryInMinutes, 0);
            return new CacheItemPolicy { UpdateCallback = a => method(a), SlidingExpiration = result };
        }

        public object Get(string key) => this.Cache[key];

        public void Set(string key, object cacheItem, int expiryInMinutes)
        {
            var policy = new CacheItemPolicy
            {
                SlidingExpiration = new TimeSpan(0, expiryInMinutes, 0)
            };

            if (this.Cache.Contains(key))
            {
                this.Cache.Remove(key);
            }

            this.Cache.Add(new CacheItem(key, cacheItem), policy);
        }

        public void Set(string key, object cacheItem, CachePriority cacheItemPriority, int expiryInMinutes)
        {
            var policy = new CacheItemPolicy
            {
                SlidingExpiration = new TimeSpan(0, expiryInMinutes, 0),
                Priority = cacheItemPriority == CachePriority.Default ? CacheItemPriority.Default : CacheItemPriority.NotRemovable
            };
            this.Cache.Set(key, cacheItem, policy);
        }

        public void Set(string key, object cacheItem, CachePriority cacheItemPriority, int expiry, Action<CacheEntryUpdateArguments> method)
        {
            var policy = new CacheItemPolicy
            {
                SlidingExpiration = new TimeSpan(0, expiry, 0),
                UpdateCallback = a => method(a),
                Priority = cacheItemPriority == CachePriority.Default ? CacheItemPriority.Default : CacheItemPriority.NotRemovable
            };
            this.Cache.Set(key, cacheItem, policy);
        }

        public bool IsSet(string key) => this.Cache[key] != null;

        public void Invalidate(string key)
        {
            this.Cache.Remove(key);
        }

        public void InvalidateByPattern(string keyPattern)
        {
            var keysToInvalidate = this
                .Cache
                .Where(kv => kv.Key.StartsWith(keyPattern))
                .Select(kv => kv.Key)
                .ToList();

            foreach (var key in keysToInvalidate)
            {
                this.Invalidate(key);
            }
        }
    }
}