using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using Portolo.Framework.Common.Caching;
using Portolo.Framework.Security;
using Portolo.Security;
using Portolo.Security.Request;
using Portolo.Utility.Logging;

namespace Portolo.Framework.Web
{
    public class UserSessionService : IUserSessionService
    {
        private static readonly ICacheWrapper CacheWrapper;
        private readonly ISecurityService securityService;

        static UserSessionService()
        {
            CacheWrapper = CacheWrapperFactory.Create();
        }

        public UserSessionService(ISecurityService securityService)
        {
            this.securityService = securityService;
        }

        public void SetCurrentNavId(string navId)
        {
            // exit early, no user authenticated
            if (!(HttpContext.Current?.User is UserPrincipal user))
            {
                return;
            }

            // only attempt if it is a valid nav value
            if (int.TryParse(navId, out var navIdAsInt) && navIdAsInt > 0)
            {
                var result = this.RefreshMenuItemsFromDatabase(
                    user: user,
                    navId: navIdAsInt);

                if (result.TopNavigationItems != null && result.TopNavigationItems.Any())
                {
                    // set the top nav id in case it is required to be fetched outside of cache
                    var cacheKeyBuilder = new CacheKeyBuilder(user);
                    var navIdKey = cacheKeyBuilder.ForCurrentNavId();
                    CacheWrapper.Set(navIdKey, navId);
                }
            }
        }

        public UserNavigations GetLeftNavigation()
        {
            // exit early, no user authenticated
            if (!(HttpContext.Current?.User is UserPrincipal user))
            {
                return null;
            }

            if (user.CurrentCompanyId == null)
            {
                // no company selected, no left navigation to load
                return null;
            }

            var key = new CacheKeyBuilder(user).ForLeftNavigation();
            UserNavigations cached = null;
            try
            {
                cached = CacheWrapper.Get<UserNavigations>(key);
            }
            catch (System.Exception ex)
            {
                Log(user.UserId, $"Failed to get the UserNavigations from cache: {ex.ToString()}");
            }

            if (cached != null)
            {
                return cached;
            }

            // TODO: add `GetOrAdd` method to CacheWrapper for atomic operations
            //var response = this.securityService
            //    .GetMenu(
            //        new MenuRequestDTO
            //        {
            //            userId = user.UserId,
            //            companyId = user.CurrentCompanyId,
            //            applicationId = Convert.ToInt32(ConfigurationManager.AppSettings["ApplicationId"]) // TODO: move to new configuration
            //        });

            //if (response != null && response.Count > 0)
            //{
            //    var menuItems = response
            //        .Select(ni =>
            //            new UserNavigation
            //            {
            //                NavigationId = ni.NavigationID,
            //                NavigationText = ni.MenuText,
            //                NavigationIcon = ni.Icon,
            //                ParentNavigationId = ni.ParentID,
            //                NavigationCommand = ni.Command,
            //                NavigationPageName = ni.Link
            //            })
            //        .ToList();

            //    var userNavigations = new UserNavigations
            //    {
            //        MenuItems = menuItems
            //    };
            //    userNavigations.MenuItems.ForEach(u => u.ChildNavigation = userNavigations.MenuItems.Where(n => n.ParentNavigationId == u.NavigationId).ToList());
            //    userNavigations.MenuItems.RemoveAll(u => u.ParentNavigationId != 0);

            //    // add to cache for next time
            //    try
            //    {
            //        CacheWrapper.Set(key, userNavigations);
            //    }
            //    catch (System.Exception ex)
            //    {
            //        Log(user.UserId, $"Failed to set the UserNavigations to cache: {ex.ToString()}");
            //    }

            //    return userNavigations;
            //}

            Log(user.UserId, "No user left navigations found in database for user.");

            return null;
        }

        public NavigationItems GetTopNavigation()
        {
            return this.GetTopNavigationAndClaimItems().TopNavigationItems;
        }

        public ClaimItems GetClaims(bool validateClaims = false)
        {
            return this.GetTopNavigationAndClaimItems(validateClaims).ClaimItems;
        }

        public void ClearSession()
        {
            // exit early, no user authenticated
            if (!(HttpContext.Current?.User is UserPrincipal user))
            {
                return;
            }

            try
            {
                var key = new CacheKeyBuilder(user);
                CacheWrapper.DeleteByPattern(key.Prefix);
            }
            catch (System.Exception ex)
            {
                Log(user.UserId, $"Failed to delete the cache: {ex.ToString()}");
            }
        }

        public UserPrincipal GetUser()
        {
            var user = HttpContext.Current?.User;
            if (user is UserPrincipal)
            {
                return (UserPrincipal)HttpContext.Current?.User;
            }
            else
            {
                return null;
            }
        }

        private static void Log(int? userId, string message, [CallerMemberName] string method = null)
        {
            var telemetry = new Microsoft.ApplicationInsights.TelemetryClient();
            telemetry.TrackEvent(
                method,
                new Dictionary<string, string>
                {
                    ["UserId"] = userId?.ToString() ?? "nouser",
                    ["Message"] = message
                });
        }

        private static string GetControllerNameFromContext()
        {
            var routeValues = HttpContext
                .Current
                ?.Request
                ?.RequestContext
                ?.RouteData
                ?.Values;
            return routeValues?.ContainsKey("controller") ?? false
                ? routeValues["controller"].ToString()
                : string.Empty;
        }

        private (NavigationItems TopNavigationItems, ClaimItems ClaimItems) GetTopNavigationAndClaimItems(bool validateClaims = false)
        {
            // exit early, no user authenticated
            if (!(HttpContext.Current?.User is UserPrincipal user))
            {
                return (null, null);
            }

            if (user.CurrentCompanyId == null)
            {
                // no company selected, no top navigation to load
                return (null, null);
            }

            var controllerName = GetControllerNameFromContext();
            var cacheKey = new CacheKeyBuilder(user);
            var topNavigationKey = cacheKey.ForTopNavigation(controllerName);
            var claimsKey = cacheKey.ForClaims(controllerName);

            // fetch from cache first
            NavigationItems cachedTopNavigation = null;
            ClaimItems cachedClaims = null;
            try
            {
                cachedTopNavigation = CacheWrapper.Get<NavigationItems>(topNavigationKey);
                cachedClaims = CacheWrapper.Get<ClaimItems>(claimsKey);
            }
            catch (System.Exception ex)
            {
                Log(user.UserId, $"Failed to get NavigationItems or ClaimItems data from cache: {ex.ToString()}");
            }

            if (validateClaims)
            {
                if (cachedTopNavigation != null && cachedClaims.Any(x => x.ClaimType != null))
                {
                    return (cachedTopNavigation, cachedClaims);
                }
            }
            else
            {
                if (cachedTopNavigation != null)
                {
                    return (cachedTopNavigation, cachedClaims);
                }
            }

            // otherwise, attempt to refresh based on the last known value for the nav id
            var navIdKey = cacheKey.ForCurrentNavId();
            var navId = CacheWrapper.Get<string>(navIdKey);
            if (!string.IsNullOrEmpty(navId) && int.TryParse(navId, out var navIdAsInt))
            {
                return this.RefreshMenuItemsFromDatabase(
                    user: user,
                    navId: navIdAsInt);
            }

            Log(user.UserId, "No user top navigations found in database for user.");

            return (null, null);
        }

        private (NavigationItems TopNavigationItems, ClaimItems ClaimItems) RefreshMenuItemsFromDatabase(UserPrincipal user, int navId)
        {
            //var response = this.securityService
            //    .GetMenuItems(
            //        new MenuRequestDTO
            //        {
            //            userId = user.UserId,
            //            companyId = user.CurrentCompanyId,
            //            navigationId = navId
            //        });

            //if (response != null && response.Count > 0)
            //{
            //    var controllerName = GetControllerNameFromContext();
            //    var cacheKey = new CacheKeyBuilder(user);
            //    var topNavigationKey = cacheKey.ForTopNavigation(controllerName);
            //    var claimsKey = cacheKey.ForClaims(controllerName);

            //    var claimItems = new ClaimItems();
            //    claimItems.AddRange(
            //        response.Select(ni => new ClaimItem(controllerName, ni.Command)));

            //    var navigationItems = new NavigationItems();
            //    navigationItems
            //        .AddRange(
            //            response
            //                .Select(ni =>
            //                    new NavigationItem
            //                    {
            //                        Text = ni.MenuItemText,
            //                        Name = ni.MenuItemName,
            //                        Command = ni.Command,
            //                        Status = ni.Status,
            //                        Icon = ni.Icon,
            //                        Sequence = ni.Sequence
            //                    })
            //                .OrderBy(ni => ni.Sequence));

            //    CacheWrapper.Set(topNavigationKey, navigationItems);
            //    CacheWrapper.Set(claimsKey, claimItems);

            //    return (navigationItems, claimItems);
            //}

            return (null, null); // no menu items
        }

        private class CacheKeyBuilder
        {
            private readonly UserPrincipal user;

            public CacheKeyBuilder(UserPrincipal user)
            {
                this.user = user;
            }

            public string Prefix => $"{this.user.Email}_Session";

            public string ForCurrentNavId() => $"{this.Prefix}_CurrentNavId";

            public string ForLeftNavigation() => $"{this.Prefix}_LeftNavigation_{this.user.CurrentCompanyId}";

            public string ForTopNavigation(string controllerName) => $"{this.Prefix}_TopNavigation_{this.user.CurrentCompanyId}_{controllerName}";

            public string ForClaims(string controllerName) => $"{this.Prefix}_Claims_{this.user.CurrentCompanyId}_{controllerName}";
        }

        private class UserSessionServiceLogEntry : LogEntry
        {
            public override LogLevel LogLevel { get; set; } = LogLevel.Informational;

            public string UserId { get; set; }

            public System.Exception Exception { get; set; }
        }
    }
}
