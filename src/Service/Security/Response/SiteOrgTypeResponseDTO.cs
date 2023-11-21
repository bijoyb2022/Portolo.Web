using System;

namespace Portolo.Security.Response
{
    public class SiteOrgTypeResponseDTO
    {
        public int? OrganizationTypeKey { get; set; }
        public int? SiteOrgTypeKey { get; set; }
        public string SiteOrgTypeName { get; set; }
        public string OrganizationTypeDesc { get; set; }
        public int? PresentationOrder { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

    }
}
