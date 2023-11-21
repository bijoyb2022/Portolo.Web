using System.Linq;
using System.Security.Principal;

namespace Portolo.Framework.Security
{
    public class UserPrincipal : IUserPrincipal
    {
        public UserPrincipal(string userName)
        {
            this.Identity = new GenericIdentity(userName);
        }

        public string Password { get; set; }

        public string AdminType { get; set; }

        public string OwnerUser { get; set; }

        public string IfocusGroupName { get; set; }

        public bool KeepAlive { get; set; }

        public bool FederateIdentity { get; set; }

        public int? LoginCount { get; set; }

        public int? UserLogID { get; set; }

        public IIdentity Identity { get; }

        public int UserId { get; set; }

        public string Login { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public string CurrentCulture { get; set; }

        public string UserType { get; set; }

        public int? OwnerID { get; set; }

        public int? CurrentCompanyId { get; set; }
        public string DbConnection { get; set; }
        public string ServiceAccess { get; set; }

        public bool IsInRole(string role)
        {
            if (this.Role.Any(r => this.Role.Contains(r)))
            {
                return true;
            }

            return false;
        }
    }
}