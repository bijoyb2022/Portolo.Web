using System;

namespace Portolo.Security.Response
{
    public class PhoneTypesResponseDTO
    {
        public int? PhoneTypeKey { get; set; }
        public string PhoneTypeDesc { get; set; }
        public int? PresentationOrder { get; set; }
        public int? SCOrder { get; set; }
        public int? IsPrivate { get; set; }
        public int? IsCell { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

    }
}
