using Portolo.Web.App_Start;
using System.Web.Mvc;

namespace Portolo.Application.Web.Controllers
{
    public class BaseController : Controller
    {
        protected virtual new CustomPrincipal User
        {
            get { return HttpContext.User as CustomPrincipal; }
        }
    }
}