using Portolo.Security.Request;
using Protolo.Application.Web.Areas.System.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Protolo.Application.Web.Areas.System.Controllers
{
    public partial class SystemController
    {
        public ActionResult LookUpConfiguration()
        {
            return View();
        }
        public PartialViewResult AddConfigLookup(string id)
        {
            var model = new ApplicationSettingsViewModel();
            if (!string.IsNullOrEmpty(id))
            {
                model = GetSelectApplicationSettings(id);
            }
            return PartialView("_AddConfigLookup", model);
        }
        public PartialViewResult ConfigurationLookupList()
        {
            return PartialView("_ConfigLookupList", GetConfigurationLookup());
        }
        private LookupCodeViewModel GetConfigurationLookup()
        {
            var model = new LookupCodeViewModel();
            model.LookupCodeTypeList = new List<SelectListItem>();
            model.LookupCodeList = new List<LookupCodeModel>();

            model.LookupCodeTypeList = primaryService.GetLookupCode(new Portolo.Primary.Request.LookupCodeRequestDTO
            {
                OptType = "ALL",
            }).Select(a => new SelectListItem
            {
                Text = a.LookupCodeType,
                Value = a.LookupCodeKey.ToString()

            }).ToList();


            model.LookupCodeList = primaryService.GetLookupCode(new Portolo.Primary.Request.LookupCodeRequestDTO
            {
                LookupCodeType = model.LookupCodeTypeList.FirstOrDefault().Text,
                OptType = "Filter",
            }).Select(a => new LookupCodeModel()
            {
                LookupCodeKey = a.LookupCodeKey,
                LookupCodeType = a.LookupCodeType,
                CodeId = a.CodeId,
                CodeDesc = a.CodeDesc,
                DisplayCodeDesc = a.DisplayCodeDesc,
                PresentationOrder = a.PresentationOrder,
                //IsSwitch = a.IsSwitch == 1 ? true : false,
                Status = a.Status,
                CreatedBy = a.CreatedBy,
                CreatedOn = a.CreatedOn
            }).ToList();

            return model;
        }
    }
}