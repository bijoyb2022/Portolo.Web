using System;

namespace Portolo.Security.Request
{
    public class UserRequestDTO
    {
        public int? UserID { get; set; }
        public int? LoginID { get; set; }
        public int? SalutationKey { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int? SuffixesKey { get; set; }
        public string UserName { get; set; }
        public int? SendEmailToKey { get; set; }
        public int? EmailTypeKey1 { get; set; }
        public string Email1 { get; set; }
        public int? EmailTypeKey2 { get; set; }
        public string Email2 { get; set; }
        public int? SendEmailToKey2 { get; set; }
        public string Password { get; set; }
        public int? CountryKey { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string StateKey { get; set; }
        public string PostalCode { get; set; }
        public int? CountryTimeZoneKey { get; set; }
        public int? PhoneTypeKey1 { get; set; }
        public int? PhoneTypeKey2 { get; set; }
        public string ContactType1 { get; set; }
        public string CountryISDCode1 { get; set; }
        public string ContactNumber1 { get; set; }
        public string ContactType2 { get; set; }
        public string CountryISDCode2 { get; set; }
        public string ContactNumber2 { get; set; }
        public int? OrganizationKey { get; set; }
        public string Organization { get; set; }
        public int? OrganizationTypeKey { get; set; }
        public int? SiteOrgTypeKey { get; set; }
        public string JobTitle { get; set; }
        public string DepartmentName { get; set; }
        public int? CreatedBy { get; set; }
        public string OptType { get; set; }

    }
}
