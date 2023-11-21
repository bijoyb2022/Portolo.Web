using System;

namespace Portolo.Security.Response
{
    public class HelpDTO
    {
        public int HelpID { get; set; }
        public int ApplicationID { get; set; }
        public string ApplicationName { get; set; }
        public string Description { get; set; }
        public int? ContextID { get; set; }
        public string ContextDescription { get; set; }
        public string Url { get; set; }
        public string Status { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
