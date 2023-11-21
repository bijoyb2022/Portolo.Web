using Portolo.Security.Request;
using Protolo.Application.Web.Areas.System.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Protolo.Application.Web.Areas.System.Controllers
{
    public partial class SystemController
    {
        public ActionResult TextConfiguration()
        {
            return View();
        }
        public PartialViewResult ConfigurationTextList(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return PartialView("_ConfigText", GetConfigurationText());
            }
            else
            {
                var result = systemService.GetApplicationText(new Portolo.Systems.Request.ApplicationTextRequestDTO
                {
                    ApplicationTextDesc = id,
                    OptType = "GET",
                }).Select(a => new ApplicationTextViewModel()
                {
                    ApplicationTextKey = a.ApplicationTextKey,
                    ApplicationTextDesc = a.ApplicationTextDesc,
                    SettingValue = a.SettingValue,
                    UserEditable = a.UserEditable,
                    SiteName = a.SiteName,
                    //IsSwitch = a.IsSwitch == 1 ? true : false,
                    SLNo = a.SLNo,
                    Status = a.Status,
                    CreatedBy = a.CreatedBy,
                    CreatedOn = a.CreatedOn
                }).ToList();

                return PartialView("_ConfigText", result);
            }
        }
        private List<ApplicationTextViewModel> GetConfigurationText()
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
                SLNo = a.SLNo,
                //IsSwitch = a.IsSwitch == 1 ? true : false,
                Status = a.Status,
                CreatedBy = a.CreatedBy,
                CreatedOn = a.CreatedOn
            }).ToList();

            return result;
        }

        public PartialViewResult _EditConfigText(string id)
        {
            return PartialView("_EditConfigText");
        }
    }
}