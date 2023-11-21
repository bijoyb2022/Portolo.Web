using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Portolo.Web;
using Portolo.Web.App_Start;
using Portolo.Web.Models;

namespace Portolo.Application.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            WebContainerFactory.ConfigureContainer();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {

                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                CustomPrincipalSerializeModel serializeModel = JsonConvert.DeserializeObject<CustomPrincipalSerializeModel>(authTicket.UserData);
                CustomPrincipal newUser = new CustomPrincipal(authTicket.Name);
                newUser.LoginID = serializeModel.LoginID;
                newUser.Name = serializeModel.Name;
                newUser.roles = serializeModel.roles;
                newUser.MobileNo = serializeModel.MobileNo;
                newUser.Email = serializeModel.Email;
                serializeModel.Password = serializeModel.Password;
                newUser.UserType = serializeModel.UserType;
                newUser.UserCode = serializeModel.UserCode;
                newUser.MyBalance = serializeModel.MyBalance;
                newUser.ProfileImg = serializeModel.ProfileImg;
                HttpContext.Current.User = newUser;
            }
        }
        protected void Application_EndRequest()
        {
            if (Context.Items["AjaxPermissionDenied"] is bool)
            {
                Context.Response.StatusCode = 401;
                Context.Response.End();
            }
        }
    }
}
