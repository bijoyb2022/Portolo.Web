using System;
using System.Configuration;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;
using Portolo.Framework.Utils;

namespace Portolo.Framework.Security
{
    public static class AuthenticationManager
    {
        /// <summary>
        /// Create a FormsAuthenticationTicket and encrypt it into a Forms Authentication cookie once the user has been authenticated.
        /// </summary>
        /// <typeparam name="T">Type of user entity.</typeparam>
        /// <param name="entity">User object.</param>
        public static void AuthenticateUser<T>(T entity)
            where T : class
        {
            var authenticatedData = JsonConvert.SerializeObject(entity);
            var authTicket = new FormsAuthenticationTicket(1,
                                                           entity.GetPropertyValue<string>("Login"),
                                                           DateTime.Now,
                                                           DateTime.Now.AddMinutes(ConfigurationManager.AppSettings["CacheTime"].ToInt()),
                                                           entity.GetPropertyValue<bool>("KeepAlive"),
                                                           authenticatedData);
            var encTicket = FormsAuthentication.Encrypt(authTicket);
            var faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            faCookie.Secure = true;
            faCookie.HttpOnly = true;
            // var cookieSize = Encoding.UTF8.GetByteCount(faCookie.Values.ToString());
            HttpContext.Current.Response.Cookies.Set(faCookie);
        }

        /// <summary>
        /// Reading FormsAuthenticationTicket,Decrypt and replacing HttpContext.User and Thread.CurrentPrincipal object.
        /// </summary>
        /// <typeparam name="T">Type of user entity.</typeparam>
        public static void AuthenticateRequest<T>()
            where T : class
        {
            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                var authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                var serializeModel = JsonConvert.DeserializeObject<T>(authTicket.UserData);

                var authenticatedUser = new UserPrincipal(authTicket.Name);

                authenticatedUser.UserId = serializeModel.GetPropertyValue<int>("UserId");
                authenticatedUser.Login = serializeModel.GetPropertyValue<string>("Login");
                authenticatedUser.UserType = serializeModel.GetPropertyValue<string>("UserType");
                authenticatedUser.AdminType = serializeModel.GetPropertyValue<string>("AdminType");
                authenticatedUser.OwnerID = serializeModel.GetPropertyValue<int?>("OwnerID");
                authenticatedUser.OwnerUser = serializeModel.GetPropertyValue<string>("OwnerUser");
                authenticatedUser.Email = serializeModel.GetPropertyValue<string>("Email");
                authenticatedUser.FirstName = serializeModel.GetPropertyValue<string>("FirstName");
                authenticatedUser.LastName = serializeModel.GetPropertyValue<string>("LastName");
                authenticatedUser.CurrentCulture = serializeModel.GetPropertyValue<string>("CurrentCulture");
                authenticatedUser.CurrentCompanyId = serializeModel.GetPropertyValue<int?>("CurrentCompanyId");
                authenticatedUser.IfocusGroupName = serializeModel.GetPropertyValue<string>("IfocusGroupName");
                authenticatedUser.Password = serializeModel.GetPropertyValue<string>("Password");
                authenticatedUser.LoginCount = serializeModel.GetPropertyValue<int?>("LoginCount");
                authenticatedUser.UserLogID = serializeModel.GetPropertyValue<int?>("UserLogID");
                authenticatedUser.KeepAlive = serializeModel.GetPropertyValue<bool>("KeepAlive");
                authenticatedUser.FederateIdentity = serializeModel.GetPropertyValue<bool>("FederateIdentity");
                authenticatedUser.DbConnection = serializeModel.GetPropertyValue<string>("DbConnection");
                authenticatedUser.ServiceAccess = serializeModel.GetPropertyValue<string>("ServiceAccess");
                HttpContext.Current.User = Thread.CurrentPrincipal = authenticatedUser;
            }
        }

        /// <summary>
        /// Update a FormsAuthenticationTicket and encrypt it into a Forms Authentication cookie once the user has been authenticated.
        /// </summary>
        /// <typeparam name="T">Type of user entity.</typeparam>
        /// <param name="entity">User object.</param>
        public static void UpdateAuthenticateUser<T>(T entity)
            where T : class
        {
            var authCookie = FormsAuthentication.GetAuthCookie(FormsAuthentication.FormsCookieName, true);
            var authTicket = FormsAuthentication.Decrypt(authCookie.Value);

            var authenticatedData = JsonConvert.SerializeObject(entity);
            var newAuthTicket = new FormsAuthenticationTicket(authTicket.Version,
                                                              authTicket.Name,
                                                              authTicket.IssueDate,
                                                              authTicket.Expiration,
                                                              entity.GetPropertyValue<bool>("KeepAlive"),
                                                              authenticatedData);
            var encTicket = FormsAuthentication.Encrypt(newAuthTicket);
            var faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            faCookie.Secure = true;
            faCookie.HttpOnly = true;
            HttpContext.Current.Response.Cookies.Add(faCookie);
            AuthenticateRequest<T>();
        }

        public static void SignOut()
        {
            FormsAuthentication.SignOut();

            // clear authentication cookie
            var faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, string.Empty);
            faCookie.Expires = DateTime.Now.AddYears(-1);
            faCookie.Secure = true;
            faCookie.HttpOnly = true;
            HttpContext.Current.Response.Cookies.Add(faCookie);
            FormsAuthentication.RedirectToLoginPage();
        }
    }
}