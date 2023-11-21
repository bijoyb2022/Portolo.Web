using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portolo.Primary.Data
{
    [Table("OrganizationType")]
    public partial class OrganizationType
    {
        public int? OrganizationTypeKey { get; set; }
        public int? EntityTypeKey { get; set; }
        public int? UnitTypeKey { get; set; }
        public string OrganizationTypeDesc { get; set; }
        public int? TreasureHuntSegment { get; set; }
        public int? PresentationOrder { get; set; }
        public int? SortSC { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
