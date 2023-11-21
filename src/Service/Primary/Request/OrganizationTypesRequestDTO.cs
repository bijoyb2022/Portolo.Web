using System;

namespace Portolo.Primary.Request
{
    public class OrganizationTypesRequestDTO
    {
        public int? OrganizationTypeKey { get; set; }
        public int? EntityTypeKey { get; set; }
        public int? UnitTypeKey { get; set; }
        public string OrganizationTypeDesc { get; set; }
        public string OptType { get; set; }

    }
}
