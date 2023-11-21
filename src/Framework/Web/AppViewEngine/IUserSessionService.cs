using Portolo.Framework.Security;

namespace Portolo.Framework.Web
{
    public interface IUserSessionService
    {
        void SetCurrentNavId(string navId);

        UserNavigations GetLeftNavigation();

        NavigationItems GetTopNavigation();

        ClaimItems GetClaims(bool validateClaim = false);

        void ClearSession();
        UserPrincipal GetUser();
    }
}