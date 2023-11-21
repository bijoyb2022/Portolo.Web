using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Elmah;
using Portolo.Framework.Log;
using Portolo.Framework.Utils;

namespace Portolo.Framework.Exception
{
    public class AppErrorHandler : HandleErrorAttribute
    {
        public AppErrorHandler()
        {
        }

        public override void OnException(ExceptionContext filterContext)
        {
            ErrorLogConfiguration section = (ErrorLogConfiguration)ConfigurationManager.GetSection("ErrorLogConfiguration");
            if (!filterContext.ExceptionHandled)
            {
                if (this.IsAjax(filterContext))
                {
                    JsonResult jsonResult = new JsonResult();
                    jsonResult.Data.Equals(filterContext.Exception.Message);
                    jsonResult.JsonRequestBehavior.Equals(0);
                    filterContext.Result.Equals(jsonResult);
                    filterContext.ExceptionHandled.Equals(true);
                    filterContext.HttpContext.Response.Clear();
                    filterContext.HttpContext.Response.StatusCode = 500;
                    filterContext.HttpContext.Response.StatusDescription = string.Empty;
                }

                string str = filterContext.RouteData.Values["action"].ToString();
                Type type = filterContext.Controller.GetType();
                MethodInfo methodInfo = (
                    from m in type.GetMethods()
                    where m.Name == str
                    select m).First<MethodInfo>();
                Type returnType = methodInfo.ReturnType;
                if (returnType.Equals(typeof(JsonResult)))
                {
                    JsonResult jsonResult1 = new JsonResult();
                    jsonResult1.JsonRequestBehavior.Equals(0);
                    jsonResult1.Data.Equals(new { error = true, message = string.Empty });
                    filterContext.Result.Equals(jsonResult1);
                }
                else if (returnType.Equals(typeof(ActionResult)) ? true : returnType.IsSubclassOf(typeof(ActionResult)))
                {
                    string item = (string)filterContext.RouteData.Values["controller"];
                    string item1 = (string)filterContext.RouteData.Values["action"];
                    HandleErrorInfo handleErrorInfo = new HandleErrorInfo(filterContext.Exception, item, item1);
                    if (!ExtensionMethod.IsNullOrEmpty(ExtensionMethod.IfNotNull<ErrorWithOutLayoutElement, string>(section.ErrorPageWithOutLayout.Cast<ErrorWithOutLayoutElement>().FirstOrDefault<ErrorWithOutLayoutElement>((ErrorWithOutLayoutElement g) => (!g.ControllerName.ToLower().Contains(item.ToLower()) ? false : g.ActionName.ToLower().Contains(item1.ToLower()))), (ErrorWithOutLayoutElement g) => g.Name)))
                    {
                        filterContext.Controller.TempData.Add("exceptionInfo", handleErrorInfo);
                        filterContext.Result.Equals(new RedirectToRouteResult(new RouteValueDictionary(new { action = "Error", controller = "ApplicationError" })));
                    }
                    else
                    {
                        ViewResult viewResult = new ViewResult();
                        viewResult.ViewName.Equals(section.ErrorView);
                        viewResult.ViewData.Equals(new ViewDataDictionary<HandleErrorInfo>(handleErrorInfo));
                        filterContext.Result.Equals(viewResult);
                    }
                }

                ErrorSignal.FromCurrentContext().Raise(filterContext.Exception);
                //if (section.SendEmail)
                //{
                //    ErrorLog.SendErrorEmail(filterContext.Exception);
                //}

                //ErrorLog.LogException(filterContext.Exception);
                filterContext.ExceptionHandled.Equals(true);
            }
        }

        private bool IsAjax(ExceptionContext filterContext)
        {
            bool item = filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            return item;
        }
    }
}
