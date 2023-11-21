using System.Web;
using System.Web.Mvc;
using Elmah;

namespace Portolo.Framework.Exception
{
    public class AppErrorResult : ActionResult
    {
        private readonly string resouceType;

        public AppErrorResult(string resouceType)
        {
            this.resouceType = resouceType;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var factory = new ErrorLogPageFactory();

            if (!string.IsNullOrEmpty(this.resouceType))
            {
                var pathInfo = "." + this.resouceType;
                HttpContext.Current.RewritePath(this.PathForStylesheet(), pathInfo, HttpContext.Current.Request.QueryString.ToString());
            }

            var httpHandler = factory.GetHandler(HttpContext.Current, null, null, null);
            httpHandler.ProcessRequest(HttpContext.Current);
        }

        private string PathForStylesheet() =>
            this.resouceType != "stylesheet"
                ? HttpContext.Current.Request.Path.Replace(string.Format("/{0}", this.resouceType), string.Empty)
                : HttpContext.Current.Request.Path;
    }
}