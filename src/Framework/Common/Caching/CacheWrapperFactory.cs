using Portolo.Utility.Configuration;

namespace Portolo.Framework.Common.Caching
{
    public static class CacheWrapperFactory
    {
        public static ICacheWrapper Create()
        {
            if (ConfigurationUtility.Current.GetSection<CacheConfigurationSection>().Type == CacheConfigurationSection.CacheType.Redis)
            {
                return new RedisCacheWrapper();
            }
            else
            {
                return new InMemoryCacheWrapper(new MemoryCacheProvider());
            }
        }
    }
}
