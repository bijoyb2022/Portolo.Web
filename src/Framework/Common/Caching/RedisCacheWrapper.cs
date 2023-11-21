using System;
using Portolo.Utility.Configuration;
using StackExchange.Redis;

namespace Portolo.Framework.Common.Caching
{
    public sealed class RedisCacheWrapper : CacheWrapper
    {
        private static readonly Lazy<ConnectionMultiplexer> LazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            var configuration = ConfigurationUtility
                .Current
                .GetSection<CacheConfigurationSection>();

            return ConnectionMultiplexer.Connect(configuration.Redis.ConnectionString);
        });

        private ConnectionMultiplexer redisConnections;

        public RedisCacheWrapper()
        {
            this.InitializeConnection();
        }

        public static ConnectionMultiplexer LazyRedisConnection => LazyConnection.Value;

        private IDatabase RedisDatabase
        {
            get
            {
                if (this.redisConnections == null)
                {
                    this.InitializeConnection();
                }

                return this.redisConnections?.GetDatabase();
            }
        }

        protected override string GetValue(string key)
        {
            if (this.RedisDatabase == null)
            {
                return null;
            }

            var redisObject = this.RedisDatabase.StringGet(key);
            if (redisObject.HasValue)
            {
                return redisObject.ToString();
            }

            return null;
        }

        protected override void SetValue(string key, string objectToCache, TimeSpan? expiry = null)
        {
            if (this.RedisDatabase == null)
            {
                return;
            }

            this.RedisDatabase.StringSet(key, objectToCache, expiry);
        }

        protected override void DeleteValue(string key)
        {
            if (this.RedisDatabase == null)
            {
                return;
            }

            this.RedisDatabase.KeyDelete(key);
        }

        protected override void DeleteValueByPattern(string prefixKey)
        {
            if (this.RedisDatabase == null)
            {
                return;
            }

            foreach (var endPoint in this.redisConnections.GetEndPoints())
            {
                var server = this.redisConnections.GetServer(endPoint);
                foreach (var key in server.Keys(pattern: prefixKey))
                {
                    this.RedisDatabase.KeyDelete(key);
                }
            }
        }

        private void InitializeConnection()
        {
            try
            {
                this.redisConnections = LazyRedisConnection;
            }
            catch (RedisConnectionException errorConnectionException)
            {
                //ErrorSignal.FromCurrentContext().Raise(errorConnectionException);
            }
        }
    }
}