using System.IO;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Xsl;
using Portolo.Framework.Security;
using Portolo.Framework.Utils;

namespace Portolo.Framework.Web
{
    // TODO: consolidate logic for BaseViewPage and BaseViewPage<T> into a single class
    public abstract class BaseViewPage<TModel> : WebViewPage<TModel>
    {
        private Configs applicationConfigs;
        private UserAttributes userAttributes;
        public new virtual UserPrincipal User => base.User as UserPrincipal;

        public Configs ApplicationConfigs
        {
            get
            {
                try
                {
                    this.applicationConfigs = (Configs)this.ViewBag.ApplicationConfigs;
                }
                catch (System.Exception)
                {
                    this.applicationConfigs = null;
                }

                return this.applicationConfigs;
            }
        }

        public UserAttributes UserAttributes
        {
            get
            {
                try
                {
                    this.userAttributes = (UserAttributes)this.ViewBag.UserAttributes;
                }
                catch (System.Exception)
                {
                    this.userAttributes = null;
                }

                return this.userAttributes;
            }
        }

        public override void InitHelpers()
        {
            base.InitHelpers();

            var userSessionService = DependencyResolver
                .Current
                .GetService<IUserSessionService>();

            var leftNavigation = userSessionService.GetLeftNavigation();
            if (leftNavigation != null)
            {
                var inputXml = leftNavigation.ToXml();
                var xsltTemplate = HostingEnvironment.MapPath("~/Template/default.left.menu.xslt");
                var argsList = new XsltArgumentList();
                var transform = new XslCompiledTransform();
                transform.Load(xsltTemplate);

                using (var sr = new StringReader(inputXml))
                {
                    using (var xr = XmlReader.Create(sr))
                    {
                        using (var sw = new StringWriter())
                        {
                            transform.Transform(xr, argsList, sw);
                            this.ViewBag.LeftNavigation = sw.ToString();
                        }
                    }
                }
            }

            var topNavigation = userSessionService
                .GetTopNavigation();
            if (topNavigation != null)
            {
                var actionName = this.Url.RequestContext.RouteData.Values["action"].ToString();
                var controllerName = this.Url.RequestContext.RouteData.Values["controller"].ToString();
                var areaName = this.Url.RequestContext.RouteData.DataTokens["area"] != null
                    ? this.Url.RequestContext.RouteData.DataTokens["area"].ToString()
                    : string.Empty;

                var inputXml = topNavigation.ToXml();
                var xsltTemplate = HostingEnvironment.MapPath("~/Template/default.top.menu.xslt");
                var argsList = new XsltArgumentList();
                argsList.AddParam("area", string.Empty, areaName);
                argsList.AddParam("controller", string.Empty, controllerName);
                argsList.AddParam("action", string.Empty, actionName);
                var transform = new XslCompiledTransform();
                transform.Load(xsltTemplate);

                using (var sr = new StringReader(inputXml))
                {
                    using (var xr = XmlReader.Create(sr))
                    {
                        using (var sw = new StringWriter())
                        {
                            transform.Transform(xr, argsList, sw);
                            this.ViewBag.TopNavigation = sw.ToString();
                        }
                    }
                }
            }
        }

        public override void Execute()
        {
        }
    }
}
