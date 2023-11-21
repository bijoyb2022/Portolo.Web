using Portolo.Primary;
using Portolo.Security;
using Portolo.Systems;
using System.Web.Mvc;

namespace Protolo.Application.Web.Areas.System.Controllers
{
    public partial class SystemController : Controller
    {
        private readonly ISystemService systemService;
        private readonly IPrimaryService primaryService;
        public SystemController(ISystemService systemService, IPrimaryService primaryService)
        {
            this.systemService = systemService;
            this.primaryService = primaryService;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }
    }
}