using System;

namespace Portolo.Security.Request
{
    public class SiteOrgTypeRequestDTO
    {
        public int? OrganizationTypeKey { get; set; }
        public int? SiteOrgTypeKey { get; set; }
        public string SiteOrgTypeName { get; set; }
        public string OrganizationTypeDesc { get; set; }
        public string OptType { get; set; }

    }
}
