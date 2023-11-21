using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Portolo.Web.Models
{
    public class LogInViewModel
    {
        [Required(ErrorMessage = "Please Enter User Name.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please Enter Password.")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string UserType { get; set; }

        public string EmailID { get; set; }

        public string MobileNo { get; set; }

        public long? LoginCount { get; set; }

        public string IPAddress { get; set; }
    }
}