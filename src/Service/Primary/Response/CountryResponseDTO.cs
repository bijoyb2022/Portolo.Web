using System;

namespace Portolo.Primary.Response
{
    public class CountryResponseDTO
    {
        public int? CountryKey { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string ISDCode { get; set; }
        public string WeekendDays { get; set; }
        public int? PresentationOrder { get; set; }
        public string Status { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
