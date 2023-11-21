using System;

namespace Portolo.Primary.Response
{
    public class StateResponseDTO
    {
        public int? StateKey { get; set; }
        public int? CountryKey { get; set; }
        public string CountryName { get; set; }
        public string StateCode { get; set; }
        public string StateName { get; set; }
        public int? DefaultTimezoneKey { get; set; }
        public decimal? UTCoffset { get; set; }
        public string UTCDSToffset { get; set; }
        public bool? IsDirectoriesStates { get; set; }
        public string Status { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
