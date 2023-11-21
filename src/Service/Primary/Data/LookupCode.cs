using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portolo.Primary.Data
{
    [Table("LookupCode")]
    public partial class LookupCode
    {
        public int? LookupCodeKey { get; set; }
        public string CodeId { get; set; }
        public string CodeDesc { get; set; }
        public string DisplayCodeDesc { get; set; }
        public int? PresentationOrder { get; set; }
        public string LookupCodeType { get; set; }
        public string Status { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
