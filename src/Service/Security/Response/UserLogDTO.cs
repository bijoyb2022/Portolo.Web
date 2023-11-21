using System;

namespace Portolo.Security.Response
{
    public class UserLogDTO
    {
        public int UserLogID { get; set; }
        public int UserID { get; set; }
        public int ApplicationID { get; set; }
        public string LoginIP { get; set; }
        public DateTime? Login { get; set; }
        public DateTime? Logout { get; set; }
        public string SessionID { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
