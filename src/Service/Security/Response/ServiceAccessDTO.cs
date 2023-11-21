using System;

namespace Portolo.Security.Response
{
    public class ServiceAccessDTO
    {
        public int ServiceAccessID { get; set; }
        public int UserID { get; set; }
        public int ServiceModuleID { get; set; }
        public int OwnerID { get; set; }
        public string LicenceKey { get; set; }
        public string Status { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool HasAccess { get; set; }
    }
}
