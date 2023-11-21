using Newtonsoft.Json;
using Portolo.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Portolo.Primary;
using Portolo.Security;

namespace Portolo.Application.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ISecurityService securityService;
        private readonly IPrimaryService primaryService;
        public HomeController(ISecurityService securityService, IPrimaryService primaryService)
        {
            this.securityService = securityService;
            this.primaryService = primaryService;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(RegistrationViewModel model) 
        {
            var user = securityService.ValidateUsers(new Security.Request.UserRequestDTO()
            {
                UserName = model.Email1,
                Password = model.Password,
                OptType = "GET",
            });

            if (user.LoginID > 0)
            {
                CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
                serializeModel.LoginID = user.LoginID;
                serializeModel.Name = user.FirstName;
                serializeModel.Email = user.Email1;
                serializeModel.Password = model.Password;
                //serializeModel.ProfileImg = System.IO.File.Exists(Server.MapPath("~/Upload/displayProfile/") + user.UserCode + ".png") ? Url.Content("/Upload/displayProfile/" + Path.GetFileName(user.UserCode + ".png")) : Url.Content("/Content/assets/images/user/" + Path.GetFileName("blank.jpg"));
                string userData = JsonConvert.SerializeObject(serializeModel);
                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                1,
                user.Email1,
                DateTime.Now,
                DateTime.Now.AddMinutes(60),
                false, //pass here true, if you want to implement remember me functionality
                userData);

                string encTicket = FormsAuthentication.Encrypt(authTicket);
                HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                Response.Cookies.Add(faCookie);
                model.UserType = "A";
            }

            if (!string.IsNullOrEmpty(model.UserType))
            {
                return Json(model.UserType.ToUpper(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(user.LoginID == 0 ? "Wrong User" : user.LoginID == -1 ? "Wrong Password" : "", JsonRequestBehavior.AllowGet);
            }
        }

        [AllowAnonymous]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            Response.AddHeader("Cache-control", "no-store, must-revalidate, private, no-cache");
            Response.AddHeader("Pragma", "no-cache");
            Response.AddHeader("Expires", "0");
            Response.AppendToLog("window.location.reload();");

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public ActionResult MyProfile()
        {
            var model = new RegistrationViewModel();

            model.SalutationList = new List<SelectListItem>();
            model.SalutationList = primaryService.GetLookupCode(new Primary.Request.LookupCodeRequestDTO
            {
                LookupCodeType = "Salutations",
                OptType = "FILTER",
            }).Select(a => new SelectListItem
            {
                Text = a.DisplayCodeDesc,
                Value = a.LookupCodeKey.ToString()
            }).ToList();

            model.SuffixList = new List<SelectListItem>();
            model.SuffixList = primaryService.GetLookupCode(new Primary.Request.LookupCodeRequestDTO
            {
                LookupCodeType = "Suffixes",
                OptType = "FILTER",
            }).Select(a => new SelectListItem
            {
                Text = a.DisplayCodeDesc,
                Value = a.LookupCodeKey.ToString()
            }).ToList();

            model.SendEmailToList = new List<SelectListItem>();
            model.SendEmailToList = primaryService.GetLookupCode(new Primary.Request.LookupCodeRequestDTO
            {
                LookupCodeType = "SendEmailTo",
                OptType = "FILTER",
            }).Select(a => new SelectListItem
            {
                Text = a.DisplayCodeDesc,
                Value = a.LookupCodeKey.ToString()
            }).ToList();


            model.SendEmailTypeList = new List<SelectListItem>();
            model.SendEmailTypeList = primaryService.GetLookupCode(new Primary.Request.LookupCodeRequestDTO
            {
                LookupCodeType = "EmailTypes",
                OptType = "FILTER",
            }).Select(a => new SelectListItem
            {
                Text = a.DisplayCodeDesc,
                Value = a.LookupCodeKey.ToString()
            }).ToList();

            model.SendEmailTypeSecondList = new List<SelectListItem>();
            model.SendEmailTypeSecondList = primaryService.GetLookupCode(new Primary.Request.LookupCodeRequestDTO
            {
                LookupCodeType = "EmailTypes",
                OptType = "FILTER",
            }).Select(a => new SelectListItem
            {
                Text = a.DisplayCodeDesc,
                Value = a.LookupCodeKey.ToString()
            }).ToList();

            var sendEmailTypeList = primaryService.GetLookupCode(new Primary.Request.LookupCodeRequestDTO
            {
                LookupCodeType = "EmailTypes",
                OptType = "FILTER",
            }).ToList();

            model.EmailTypeKey1 = sendEmailTypeList.Where(a => a.DisplayCodeDesc == "Office").FirstOrDefault().LookupCodeKey;
            model.EmailTypeKey2 = sendEmailTypeList.Where(a => a.DisplayCodeDesc == "Personal").FirstOrDefault().LookupCodeKey;

            model.CountryList = new List<SelectListItem>();
            var countryList = primaryService.GetCountry(new Primary.Request.CountryRequestDTO
            {
                OptType = "GET"
            }).ToList();

            model.CountryList = countryList.Select(a => new SelectListItem
            {
                Text = a.CountryName,
                Value = a.CountryKey.ToString()

            }).ToList();

            
            model.StateList = new List<SelectListItem>();
            model.StateList = primaryService.GetState(new Primary.Request.StateRequestDTO
            {
                CountryKey = countryList.FirstOrDefault().CountryKey,
                OptType = "GET"

            }).Select(a => new SelectListItem
            {
                Text = a.StateName,
                Value = a.StateKey.ToString()

            }).ToList();


            model.CountryTimeZoneList = new List<SelectListItem>();
            model.CountryTimeZoneList = primaryService.GetCountryTimeZone(new Primary.Request.CountryTimeZoneRequestDTO
            {
                CountryKey = countryList.FirstOrDefault().CountryKey,
                OptType = "GET"

            }).Select(a => new SelectListItem
            {
                Text = a.TimeZone,
                Value = a.CountryTimeZoneKey.ToString()
            }).ToList();
            

            var phoneTypeList = primaryService.GetPhoneTypes(new Primary.Request.PhoneTypesRequestDTO
            {
                OptType = "GET"
            }).ToList();
            model.PhoneTypeList = phoneTypeList.Select(a => new SelectListItem
            {
                Text = a.PhoneTypeDesc,
                Value = a.PhoneTypeKey.ToString()

            }).ToList();

            model.PhoneTypeSecondList = phoneTypeList.Select(a => new SelectListItem
            {
                Text = a.PhoneTypeDesc,
                Value = a.PhoneTypeKey.ToString()

            }).ToList();

            model.PhoneTypeKey1 = phoneTypeList.Where(a => a.PhoneTypeDesc == "Mobile").FirstOrDefault().PhoneTypeKey;
            model.PhoneTypeKey2 = phoneTypeList.Where(a => a.PhoneTypeDesc == "Direct").FirstOrDefault().PhoneTypeKey;

            model.OrganizationTypeList = new List<SelectListItem>();
            model.OrganizationTypeList = primaryService.GetOrganizationTypes(new Primary.Request.OrganizationTypesRequestDTO
            {
                OptType = "GET"
            }).Select(a => new SelectListItem
            {
                Text = a.OrganizationTypeDesc,
                Value = a.OrganizationTypeKey.ToString()

            }).ToList();

            model.CountryISDCode1 = countryList.Where(a => a.CountryKey == countryList.FirstOrDefault().CountryKey).FirstOrDefault().ISDCode;
            model.CountryISDCode2 = countryList.Where(a => a.CountryKey == countryList.FirstOrDefault().CountryKey).FirstOrDefault().ISDCode;

            //model.SiteTypeList = new List<SelectListItem>();
            //model.SiteTypeList = securityService.GetSiteTypes().Select(a => new SelectListItem
            //{
            //    Text = a.SiteTypeDesc,
            //    Value = a.SiteTypeKey.ToString()

            //}).ToList();

            return View(model);
        }

        [HttpPost]
        public JsonResult GetSiteOrgType(RegistrationViewModel model)
        {
            ////var result = securityService.GetSiteOrgType(new SiteOrgTypeRequestDTO
            ////{
            ////    OrganizationTypeKey = !string.IsNullOrEmpty(model.OrganizationTypeKey.ToString()) ? model.OrganizationTypeKey : -1,
            ////    OptType = "GET",
            ////}).ToList();

            ////return Json(result, JsonRequestBehavior.AllowGet);
            ///
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveRegistration(RegistrationViewModel model)
        {
            var result = securityService.SaveUser(new Security.Request.UserRequestDTO()
            {
                SalutationKey = model.SalutationKey,   
                FirstName = model.FirstName,
                MiddleName = model.LastName,
                LastName = model.LastName,
                SuffixesKey= model.SuffixesKey,
                UserName = model.UserName,
                SendEmailToKey = model.SendEmailToKey,
                EmailTypeKey1= model.EmailTypeKey1,
                Email1=model.Email1,
                EmailTypeKey2 = model.EmailTypeKey2,
                Email2= model.Email2,
                Password = model.Password,
                CountryKey= model.CountryKey,
                Address1 = model.Address1,
                Address2 = model.Address2,
                City = model.City,
                StateKey = model.StateKey,
                PostalCode = model.PostalCode,
                CountryTimeZoneKey= model.CountryTimeZoneKey,
                ContactType1 = model.ContactType1,
                CountryISDCode1 = model.CountryISDCode1,
                ContactNumber1 = model.ContactNumber1,
                ContactType2 = model.ContactType2,
                CountryISDCode2 = model.CountryISDCode2,
                ContactNumber2 = model.ContactNumber2,
                Organization = model.Organization,
                OrganizationTypeKey= model.OrganizationTypeKey, 
                JobTitle= model.JobTitle,
                DepartmentName= model.DepartmentName,
                CreatedBy = (model.UserID) > 0 ? model.UserID : -1,
                UserID = (model.UserID) > 0 ? model.UserID : null,
                OptType = (model.UserID) > 0 ? "UPDATE" : "INSERT"
            });

            ///*************** Please Change Subject & Body Message For OTP Share *********************/
            //var subject = "Registration For OTP";
            //var content = "";
            //if (result.Item1 > 0)
            //{
            //    var response = a1BazarNetworkService.GetOTP(new A1BazarNetwork.Service.Request.OtpRequestDTO()
            //    {
            //        MobileNo = model.MobileNo,
            //        OtpNo = result.Item2.ToString(),
            //        OptType = "GET"
            //    });

            //    content += "<div><b>Dear " + model.UserType + ",</b></div><br />";
            //    content += "<div>Your OTP number is - " + result.Item2.ToString() + " </div><br />";
            //    content += "<div>For any other assistance please mail us at <i>support@.com</i><br /><br /></div>";

            //    content += "Thanks & Regards,";
            //    content += "<b><br /> Mail Service</b><br />";
            //    var form = System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString();

            //    //emailSender.SendHtmlEmailAsync(subject, content, form, model.EmailID, null, null);

            //    //var optText = "Dear User, OTP is " + result.Item2.ToString() + "valid for 15 minutes.Please note, in case the OTP is expired, you can Re - generate OTP. - Thanks, A1BAZAR";
            //    //SendSMS(optText, model.MobileNo);
            //}


            //return Json(result.Item1, JsonRequestBehavior.AllowGet);
            return Json(1, JsonRequestBehavior.AllowGet);
        }

    }
}