using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portolo.Web.Models
{
    public class CustomPrincipalSerializeModel
    {
        public int? LoginID { get; set; }
        public string Name { get; set; }
        public string[] roles { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public string UserCode { get; set; }
        public string ProfileImg { get; set; }
        public decimal? MyBalance { get; set; }
    }
}