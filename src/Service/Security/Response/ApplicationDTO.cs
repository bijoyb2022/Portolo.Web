using System;
using System.Collections.Generic;

namespace Portolo.Security.Response
{
    public class ApplicationDTO
    {
        public int ApplicationID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string State { get; set; }
        public string Message { get; set; }
        public string Theme { get; set; }
        public int? SessionTimeout { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public virtual ICollection<ApplicationVersionDTO> ApplicationVersions { get; set; }
    }
}
