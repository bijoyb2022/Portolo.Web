using System;

namespace Portolo.Security.Response
{
    public class ClaimXPOAuthenticationDTO
    {
        public int AuthID { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }
    }
}
