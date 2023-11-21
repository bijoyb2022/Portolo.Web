using System;
using System.Collections.Generic;

namespace Portolo.Security.Response
{
    public class UserLoginDTO
    {
        public int UserID { get; set; }
        public string LoginID { get; set; }
        public int ApplicationID { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
        public int OwnerID { get; set; }
        public string OwnerUser { get; set; }
        public string CompanyID { get; set; }
        public string UserType { get; set; }
        public string AdminType { get; set; }
        public string LoginURL { get; set; }
        public bool? FTPFlag { get; set; }
        public string FTPLogid { get; set; }
        public string FTPFolder { get; set; }
        public string FTPPassword { get; set; }
        public string Reference { get; set; }
        public string IP { get; set; }
        public int? LoginCount { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime? PasswordExpirydate { get; set; }
        public int? DbConID { get; set; }
        public int? DbConIDRateshop { get; set; }
        public int? DbConIDReports { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public virtual ApplicationDTO Application { get; set; }
        public virtual DBConnectionDTO DBConnection { get; set; }
        public virtual ICollection<UserGroupDTO> UserGroups { get; set; }
        public virtual ICollection<UserInfoDTO> UserInfoes { get; set; }
        public virtual ICollection<UserLogDTO> UserLogs { get; set; }
        public bool? TwoFactorEnabled { get; set; }
        public bool? EmailConfirmed { get; set; }
        public string LoginSessionID { get; set; }
    }
}
