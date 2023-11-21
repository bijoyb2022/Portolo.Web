using Portolo.Security.Request;
using Protolo.Application.Web.Areas.System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Protolo.Application.Web.Areas.System.Controllers
{
    public partial class SystemController
    {
        public ActionResult SettingsConfiguration()
        {
            return View();
        }
        public PartialViewResult AddConfigSettings(string id)
        {
            var model = new ApplicationSettingsViewModel();
            if (!string.IsNullOrEmpty(id))
            {
                model = GetSelectApplicationSettings(id);
            }
            return PartialView("_AddConfigSettings", model);
        }
        private ApplicationSettingsViewModel GetSelectApplicationSettings(string id)
        {
            var model = new ApplicationSettingsViewModel();
            var result = systemService.GetSelectApplicationSettings(new Portolo.Systems.Request.ApplicationSettingsRequestDTO
            {
                ApplicationSettingKey = Convert.ToInt32(id),
                OptType = "GET",
            });
            model.ApplicationSettingKey = result.ApplicationSettingKey;
            model.ApplicationSettingDesc = result.ApplicationSettingDesc;
            model.Usage = result.Usage;
            return model;
        }
        public PartialViewResult ApplicationSettingsList(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return PartialView("_ConfigSettings", GetApplicationSettings());
            }
            else
            {
                var result = systemService.GetApplicationSettings(new Portolo.Systems.Request.ApplicationSettingsRequestDTO
                {
                    ApplicationSettingDesc = id,
                    OptType = "GET",
                }).Select(a => new ApplicationSettingsViewModel()
                {
                    ApplicationSettingKey = a.ApplicationSettingKey,
                    ApplicationSettingDesc = a.ApplicationSettingDesc,
                    SettingValue = a.SettingValue,
                    UserEditable = a.UserEditable,
                    Usage = a.Usage,
                    SiteName = a.SiteName,
                    SLNo = a.SLNo,
                    //IsSwitch = a.IsSwitch == 1 ? true : false,
                    Status = a.Status,
                    CreatedBy = a.CreatedBy,
                    CreatedOn = a.CreatedOn
                }).ToList();
                return PartialView("_ConfigSettings", result);
            }

        }

        public JsonResult GetApplicationSettingsList(string id)
        {
            var result = systemService.GetApplicationSettings(new Portolo.Systems.Request.ApplicationSettingsRequestDTO
            {
                ApplicationSettingDesc = id,
                OptType = "GET",
            }).Select(a => new ApplicationSettingsViewModel()
            {
                ApplicationSettingKey = a.ApplicationSettingKey,
                ApplicationSettingDesc = a.ApplicationSettingDesc,
                SettingValue = a.SettingValue,
                UserEditable = a.UserEditable,
                Usage = a.Usage,
                SiteName = a.SiteName,
                SLNo = a.SLNo,
                Edit = "<div class='table-icon-btn'><a href=\"javascript:void(0);\" onclick=\"fnAddConfigSettings('" + a.ApplicationSettingKey + "')\"><img src='/images/edit-Icon.png' alt='edit'></a></div>",
                //IsSwitch = a.IsSwitch == 1 ? true : false,
                Status = a.Status,
                CreatedBy = a.CreatedBy,
                CreatedOn = a.CreatedOn
            }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private List<ApplicationSettingsViewModel> GetApplicationSettings()
        {
            var result = new List<ApplicationSettingsViewModel>();

            result = systemService.GetApplicationSettings(new Portolo.Systems.Request.ApplicationSettingsRequestDTO
            {
                OptType = "GET",
            }).Select(a => new ApplicationSettingsViewModel()
            {
                ApplicationSettingKey = a.ApplicationSettingKey,
                ApplicationSettingDesc = a.ApplicationSettingDesc,
                SettingValue = a.SettingValue,
                UserEditable = a.UserEditable,
                Usage = a.Usage,
                SiteName = a.SiteName,
                SLNo = a.SLNo,
                //IsSwitch = a.IsSwitch == 1 ? true : false,
                Status = a.Status,
                CreatedBy = a.CreatedBy,
                CreatedOn = a.CreatedOn
            }).ToList();

            return result;
        }

        [HttpPost]
        public JsonResult GetApplicationSettings(ApplicationSettingsViewModel model)
        {
            var result = systemService.GetApplicationSettings(new Portolo.Systems.Request.ApplicationSettingsRequestDTO
            {
                ApplicationSettingDesc = model.ApplicationSettingDesc,
                OptType = "GET",
            }).Select(a => new ApplicationSettingsViewModel()
            {
                ApplicationSettingKey = a.ApplicationSettingKey,
                ApplicationSettingDesc = a.ApplicationSettingDesc,
                SettingValue = a.SettingValue,
                UserEditable = a.UserEditable,
                Usage = a.Usage,
                SiteName = a.SiteName,
                //IsSwitch = a.IsSwitch == 1 ? true : false,
                Status = a.Status,
                CreatedBy = a.CreatedBy,
                CreatedOn = a.CreatedOn
            }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}