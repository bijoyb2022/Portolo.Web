using System;

namespace Portolo.Security.Response
{
    public class UserInfoDTO
    {
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int UserInfoID { get; set; }
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string StateCode { get; set; }
        public string Postal { get; set; }
        public string CountryCode { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneHome { get; set; }
        public string PhoneCell { get; set; }
        public string PhoneWork { get; set; }
        public string JobTitle { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string Comments { get; set; }
        public DateTime? ReferedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string AuthToken { get; set; }
        public DateTime? LastGenerateDate { get; set; }
        public string ProfileImage { get; set; }
    }
}
