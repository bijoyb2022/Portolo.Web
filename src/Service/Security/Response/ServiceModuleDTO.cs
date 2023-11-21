using System;
using System.Collections.Generic;

namespace Portolo.Security.Response
{
    public class ServiceModuleDTO
    {
        public int ServiceModuleID { get; set; }
        public string Name { get; set; }
        public int ApplicationID { get; set; }
        public string ApplicationName { get; set; }
        public string EndPointAddress { get; set; }
        public string Status { get; set; }
        public bool? UIExists { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public List<ServiceAccessDTO> ServiceAccess { get; set; }
    }
}
