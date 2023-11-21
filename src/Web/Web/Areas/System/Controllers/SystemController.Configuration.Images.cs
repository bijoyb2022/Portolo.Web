using Portolo.Security.Request;
using Protolo.Application.Web.Areas.System.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Protolo.Application.Web.Areas.System.Controllers
{
    public partial class SystemController
    {
        public ActionResult ImagesConfiguration()
        {
            return View();
        }
        public PartialViewResult ConfigurationImagesList()
        {
            return PartialView("_ConfigImages", GetConfigurationImages());
        }
        private List<ApplicationTextViewModel> GetConfigurationImages()
        {
            var result = new List<ApplicationTextViewModel>();

            result = systemService.GetApplicationText(new Portolo.Systems.Request.ApplicationTextRequestDTO
            {
                OptType = "GET",
            }).Select(a => new ApplicationTextViewModel()
            {
                ApplicationTextKey = a.ApplicationTextKey,
                ApplicationTextDesc = a.ApplicationTextDesc,
                SettingValue = a.SettingValue,
                UserEditable = a.UserEditable,
                SiteName = a.SiteName,
                //IsSwitch = a.IsSwitch == 1 ? true : false,
                Status = a.Status,
                CreatedBy = a.CreatedBy,
                CreatedOn = a.CreatedOn
            }).ToList();

            return result;
        }
    }
}