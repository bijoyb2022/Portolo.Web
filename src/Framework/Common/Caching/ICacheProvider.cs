namespace Portolo.Framework.Common.Caching
{
    public interface ICacheProvider
    {
        object Get(string key);

        void Set(string key, object data, int expiryInMinutes);

        void Set(string key, object cacheItem, CachePriority cacheItemPriority, int expiryInMinutes);

        bool IsSet(string key);

        void Invalidate(string key);

        void InvalidateByPattern(string keyPattern);
    }
}