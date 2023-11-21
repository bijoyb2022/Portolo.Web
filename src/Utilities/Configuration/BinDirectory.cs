using System;
#if NET472
using System.Web.Hosting;
#endif

namespace Portolo.Utility.Configuration
{
    public static class BinDirectory
    {
        public static string Get()
        {
#if NET472
            if (HostingEnvironment.IsHosted)
            {
                return HostingEnvironment.MapPath("~/bin");
            }
#endif

            return AppDomain.CurrentDomain.BaseDirectory;
        }
    }
}
