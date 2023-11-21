using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Portolo.Web.App_Start
{
    public class CustomPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role)
        {
            if (roles.Any(r => role.Contains(r)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public CustomPrincipal(string Username)
        {
            this.Identity = new GenericIdentity(Username);
        }

        public int? LoginID { get; set; }
        public string Name { get; set; }
        public string[] roles { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string UserType { get; set; }
        public string UserCode { get; set; }
        public string ProfileImg { get; set; }
        public decimal? MyBalance { get; set; }
    }
}