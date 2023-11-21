using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Portolo.Web.Models
{
    public class RegistrationViewModel
    {
        public RegistrationViewModel()
        {
            //this.SalutationList = new List<SelectListItem>();
            //this.SuffixList = new List<SelectListItem>();
        }
        public List<SelectListItem> SalutationList { get; set; }
        public List<SelectListItem> SuffixList { get; set; }
        public List<SelectListItem> SendEmailToList { get; set; }
        public List<SelectListItem> SendEmailTypeList { get; set; }
        public List<SelectListItem> SendEmailTypeSecondList { get; set; }
        public List<SelectListItem> CountryList { get; set; }
        public List<SelectListItem> StateList { get; set; }
        public List<SelectListItem> CountryTimeZoneList { get; set; }
        public List<SelectListItem> PhoneTypeList { get; set; }
        public List<SelectListItem> PhoneTypeSecondList { get; set; }
        public List<SelectListItem> OrganizationTypeList { get; set; }
        public List<SelectListItem> SiteTypeList { get; set; }
        public int? UserID { get; set; }
        public int? LoginID { get; set; }
        public int? SalutationKey { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int? SuffixesKey { get; set; }
        public string UserName { get; set; }
        public int? SendEmailToKey { get; set; }
        public int? EmailTypeKey { get; set; }
        public int? EmailTypeKey1 { get; set; }
        public string Email1 { get; set; }
        public int? EmailTypeKey2 { get; set; }
        public string Email2 { get; set; }
        public int? SendEmailToKey2 { get; set; }
        public string Password { get; set; }
        public int? PhoneTypeKey1 { get; set; }
        public int? PhoneTypeKey2 { get; set; }
        public int? CountryKey { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string StateKey { get; set; }
        public string PostalCode { get; set; }
        public int? CountryTimeZoneKey { get; set; }
        public string ContactType1 { get; set; }
        public string CountryISDCode1 { get; set; }
        public string ContactNumber1 { get; set; }
        public string ContactType2 { get; set; }
        public string CountryISDCode2 { get; set; }
        public string ContactNumber2 { get; set; }
        public string Organization { get; set; }
        public int? OrganizationKey { get; set; }
        public int? OrganizationTypeKey { get; set; }
        public int? SiteOrgTypeKey { get; set; }
        public string JobTitle { get; set; }
        public string DepartmentName { get; set; }
        public string UserType { get; set; }
        public string CreatedBy { get; set; }
        public int? LookupCodeKey { get; set; }
        public string DisplayCodeDesc { get; set; }
    }
}