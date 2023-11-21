using System;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.Caching;
using EnumsNET;
using Portolo.Framework.Common.Caching;
using Portolo.Framework.Utils;

namespace Portolo.Framework.Security
{
    public static class UserConfigAnalytics
    {
        private static readonly object Locker = new object();
        private static readonly ICacheWrapper Cache;

        static UserConfigAnalytics()
        {
            Cache = CacheWrapperFactory.Create();
        }

        private enum CacheKey
        {
            [Description("User_{0}.analyticsleftnavigation")]
            AnalyticsLeftNavigation,
            [Description("User_{0}.analyticstopnavigation")]
            AnalyticsTopNavigation,
        }

        public static void AnalyticsLeftNavigation(this UserPrincipal user, object value, Action<CacheEntryUpdateArguments> method)
        {
            lock (UserConfigAnalytics.Locker)
            {
                if (user != null || user.Identity.IsAuthenticated)
                {
                    object obj = CacheKey.AnalyticsLeftNavigation;
                    string[] email = new string[] { user.Email };
                    string formatedDescription = ExtensionMethod.ToFormatedDescription((Enum)obj, email);
                    UserConfigAnalytics.Cache.Set<object>(formatedDescription, value, null);
                }
            }
        }

        public static object AnalyticsLeftNavigation(this UserPrincipal user)
        {
            if (user == null && !user.Identity.IsAuthenticated)
            {
                return null;
            }

            object obj = CacheKey.AnalyticsLeftNavigation;
            string[] email = new string[] { user.Email };
            string formatedDescription = ExtensionMethod.ToFormatedDescription((Enum)obj, email);
            return UserConfigAnalytics.Cache.Get<object>(formatedDescription);
        }

        public static void AnalyticsTopNavigation(this UserPrincipal user, string key, object value, Action<CacheEntryUpdateArguments> method)
        {
            lock (UserConfigAnalytics.Locker)
            {
                if (user != null || user.Identity.IsAuthenticated)
                {
                    object obj = CacheKey.AnalyticsTopNavigation;
                    string[] email = new string[] { user.Email, key };
                    string formatedDescription = ExtensionMethod.ToFormatedDescription((Enum)obj, email);
                    UserConfigAnalytics.Cache.Set<object>(formatedDescription, value, null);
                }
            }
        }

        public static object AnalyticsTopNavigation(this UserPrincipal user, string key)
        {
            if (user == null && !user.Identity.IsAuthenticated)
            {
                return null;
            }

            object obj = CacheKey.AnalyticsTopNavigation;
            string[] email = new string[] { user.Email, key };
            string formatedDescription = ExtensionMethod.ToFormatedDescription((Enum)obj, email);
            return UserConfigAnalytics.Cache.Get<object>(formatedDescription);
        }
    }
}