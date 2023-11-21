using System;

namespace Portolo.Security.Response
{
    public class DBConnectionDTO
    {
        public int DbConID { get; set; }
        public string Name { get; set; }
        public string ConnectionString { get; set; }
        public string Status { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
