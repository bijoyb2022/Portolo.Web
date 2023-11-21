using Portolo.Utility.Configuration;

namespace Portolo.Framework.Common.Caching
{
    public class CacheConfigurationSection : ConfigurationSection
    {
        public enum CacheType
        {
            InMemory,
            Redis
        }

        public override string SectionName => "Cache";

        public CacheType Type { get; set; } = CacheType.InMemory; 

        public RedisConfiguration Redis { get; set; }

        public class RedisConfiguration
        {
            public string ConnectionString { get; set; }
        }
    }
}
