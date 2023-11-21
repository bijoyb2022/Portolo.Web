using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Portolo.Application.Web;
using Portolo.Security;
using Portolo.Primary;
using Portolo.Systems;
using Portolo.Common;
using Portolo.Organization;
using Portolo.Pages;

namespace Portolo.Web.App_Start
{
    public class WebContainerFactory
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            // Register dependencies in controllers
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<SecurityService>().As<ISecurityService>();
            builder.RegisterType<PrimaryService>().As<IPrimaryService>();
            builder.RegisterType<CommonService>().As<ICommonService>();
            builder.RegisterType<SystemService>().As<ISystemService>();          
            builder.RegisterType<OrganizationService>().As<IOrganizationService>();
            builder.RegisterType<PagesService>().As<IPagesService>();

            var container = builder.Build();

            // Set MVC DI resolver to use our Autofac container
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}