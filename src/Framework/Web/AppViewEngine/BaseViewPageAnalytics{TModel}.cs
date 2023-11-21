using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.WebPages;
using System.Xml;
using System.Xml.Xsl;
using Microsoft.CSharp.RuntimeBinder;
using Portolo.Framework.Security;
using Portolo.Framework.Utils;

namespace Portolo.Framework.Web
{
    public abstract class BaseViewPageAnalytics<TModel> : WebViewPage<TModel>
    {
        private Configs applicationConfigs;

        private UserAttributes userAttributes;

        protected BaseViewPageAnalytics()
        {
        }

        public Configs ApplicationConfigs
        {
            get
            {
                try
                {
                    this.applicationConfigs = (Configs)((dynamic)base.ViewBag()).ApplicationConfigs;
                }
                catch
                {
                    this.applicationConfigs = null;
                }

                return this.applicationConfigs;
            }
        }

        public string LeftNavigation
        {
            get;
            set;
        }

        public string ModuleTopNavigation
        {
            get;
            set;
        }

#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public virtual UserPrincipal User => base.User as UserPrincipal;
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword

        public UserAttributes UserAttributes
        {
            get
            {
                try
                {
                    this.userAttributes = (UserAttributes)((dynamic)base.ViewBag()).UserAttributes;
                }
                catch
                {
                    this.userAttributes = null;
                }

                return this.userAttributes;
            }
        }

        public override void Execute()
        {
        }

        public override void InitHelpers()
        {
            string xml;
            string str;
            XslCompiledTransform xslCompiledTransform;
            StringReader stringReader;
            XmlReader xmlReader;
            StringWriter stringWriter;
            base.InitHelpers();
            this.LeftNavigation = string.Empty;
            this.ModuleTopNavigation = string.Empty;
            if (this.User == null ? false : this.User.AnalyticsLeftNavigation() != null)
            {
                UserNavigations userNavigation = ExtensionMethod.FromJson<UserNavigations>(this.User.AnalyticsLeftNavigation());
                if (userNavigation != null)
                {
                    xml = ExtensionMethod.ToXml<UserNavigations>(userNavigation);
                    str = HostingEnvironment.MapPath("~/Template/default.left.menu.xslt");
                    xslCompiledTransform = new XslCompiledTransform();
                    xslCompiledTransform.Load(str);
                    stringReader = new StringReader(xml);
                    try
                    {
                        xmlReader = XmlReader.Create(stringReader);
                        try
                        {
                            stringWriter = new StringWriter();
                            try
                            {
                                xslCompiledTransform.Transform(xmlReader, null, stringWriter);
                                this.LeftNavigation = stringWriter.ToString();
                            }
                            finally
                            {
                                if (stringWriter != null)
                                {
                                    ((IDisposable)stringWriter).Dispose();
                                }
                            }
                        }
                        finally
                        {
                            if (xmlReader != null)
                            {
                                ((IDisposable)xmlReader).Dispose();
                            }
                        }
                    }
                    finally
                    {
                        if (stringReader != null)
                        {
                            ((IDisposable)stringReader).Dispose();
                        }
                    }
                }
            }

            if (this.User != null)
            {
                string str1 = this.Url.RequestContext.RouteData.DataTokens["area"] != null ? this.Url.RequestContext.RouteData.DataTokens["area"].ToString() : string.Empty;
                string str2 = this.Url.RequestContext.RouteData.Values["controller"].ToString();
                if (this.User.AnalyticsTopNavigation(str2.ToLower().Trim()) != null)
                {
                    NavigationItems navigationItem = ExtensionMethod.FromJson<NavigationItems>(this.User.AnalyticsTopNavigation(str2.ToLower().Trim()));
                    string str3 = this.Url.RequestContext.RouteData.Values["action"].ToString();
                    xml = ExtensionMethod.ToXml<NavigationItems>(navigationItem);
                    str = HostingEnvironment.MapPath("~/Template/default.top.menu.xslt");
                    XsltArgumentList xsltArgumentList = new XsltArgumentList();
                    xsltArgumentList.AddParam("area", string.Empty, str1);
                    xsltArgumentList.AddParam("controller", string.Empty, str2);
                    xsltArgumentList.AddParam("action", string.Empty, str3);
                    xslCompiledTransform = new XslCompiledTransform();
                    xslCompiledTransform.Load(str);
                    stringReader = new StringReader(xml);
                    try
                    {
                        xmlReader = XmlReader.Create(stringReader);
                        try
                        {
                            stringWriter = new StringWriter();
                            try
                            {
                                xslCompiledTransform.Transform(xmlReader, xsltArgumentList, stringWriter);
                                this.ModuleTopNavigation = stringWriter.ToString();
                            }
                            finally
                            {
                                if (stringWriter != null)
                                {
                                    ((IDisposable)stringWriter).Dispose();
                                }
                            }
                        }
                        finally
                        {
                            if (xmlReader != null)
                            {
                                ((IDisposable)xmlReader).Dispose();
                            }
                        }
                    }
                    finally
                    {
                        if (stringReader != null)
                        {
                            ((IDisposable)stringReader).Dispose();
                        }
                    }
                }
            }
        }
    }
}