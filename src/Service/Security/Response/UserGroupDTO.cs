using System;

namespace Portolo.Security.Response
{
    public class UserGroupDTO
    {
        public int UserGroupID { get; set; }
        public int UserID { get; set; }
        public int? OwnerID { get; set; }
        public int GroupID { get; set; }
        public int CompanyID { get; set; }
        public int ApplicationID { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public virtual ApplicationDTO Application { get; set; }
        public virtual ApplicationGroupDTO ApplicationGroup { get; set; }
        public virtual UserLoginDTO UserLogin { get; set; }
    }
}
