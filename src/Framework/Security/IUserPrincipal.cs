using System.Security.Principal;

namespace Portolo.Framework.Security
{
    public interface IUserPrincipal : IPrincipal
    {
        int UserId { get; set; }

        string Login { get; set; }

        string FirstName { get; set; }

        string LastName { get; set; }

        string Email { get; set; }

        string Role { get; set; }

        string CurrentCulture { get; set; }

        string UserType { get; set; }

        int? CurrentCompanyId { get; set; }

        int? OwnerID { get; set; }

        string DbConnection { get; set; }
        string ServiceAccess { get; set; }
    }
}