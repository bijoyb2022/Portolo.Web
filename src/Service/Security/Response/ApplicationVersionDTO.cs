using System;

namespace Portolo.Security.Response
{
    public class ApplicationVersionDTO
    {
        public int ApplicationVersionID { get; set; }
        public int ApplicationID { get; set; }
        public string VersionNumber { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public string ModulesAffected { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
