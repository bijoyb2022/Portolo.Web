using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portolo.Primary.Data
{
    [Table("CountryTimeZone")]
    public partial class CountryTimeZone
    {
        public int? CountryTimeZoneKey { get; set; }
        public string CountryCode { get; set; }
        public string Coordinates { get; set; }
        public string TimeZone { get; set; }
        public string Format { get; set; }
        public decimal? UTCoffset { get; set; }
        public string UTCDSToffset { get; set; }
        public bool? IsInternational { get; set; }
        public string TimeZoneID { get; set; }
        public string TimeZoneOld { get; set; }
        public string Status { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
