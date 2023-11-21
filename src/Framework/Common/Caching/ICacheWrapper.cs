using System;

namespace Portolo.Framework.Common.Caching
{
    public interface ICacheWrapper
    {
        void Set<T>(string key, T objectToCache, TimeSpan? expiry = null)
            where T : class;
        T Get<T>(string key)
            where T : class;
        void Delete(string key);
        void DeleteByPattern(string prefixKey);
    }
}