using System;
using System.ComponentModel;
using System.Configuration;
using EnumsNET;
using Portolo.Framework.Common.Caching;
using Portolo.Framework.Utils;

namespace Portolo.Framework.Security
{
    public static class UserConfig
    {
        private static readonly object Locker = new object();
        private static readonly ICacheWrapper Cache;

        static UserConfig()
        {
            Cache = CacheWrapperFactory.Create();
        }

        private enum CacheKey
        {
            [Description("User_{0}.currentmodule")]
            CurrentModule,
        }

        public static object GetCurrentModule(this UserPrincipal user)
        {
            if (user != null || user.Identity.IsAuthenticated)
            {
                var catchKey = CacheKey.CurrentModule.ToFormatedDescription(user.Email);
                return Cache.Get<object>(catchKey);
            }

            return null;
        }

        public static void SetCurrentModule(this UserPrincipal user, object value)
        {
            lock (Locker)
            {
                if (user != null || user.Identity.IsAuthenticated)
                {
                    var cacheProvider = SingletonCacheProvider.GetInstance;
                    var catchKey = CacheKey.CurrentModule.ToFormatedDescription(user.Email);
                    cacheProvider.Set(catchKey, value, CachePriority.NotRemovable, ConfigurationManager.AppSettings["CacheTime"].ToInt());
                }
            }
        }

        public static void AbandonConfig(this UserPrincipal user)
        {
            if (user != null || user.Identity.IsAuthenticated)
            {
                foreach (var cacheKey in Enums.GetValues<CacheKey>())
                {
                    Cache.DeleteByPattern(cacheKey.ToFormatedDescription(user.Email));
                }
            }
        }
    }
}