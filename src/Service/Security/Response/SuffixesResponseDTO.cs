using System;
using System.Collections.Generic;

namespace Portolo.Security.Response
{
    public class SuffixesResponseDTO
    {
        public int? SuffixesKey { get; set; }
        public string SuffixesName { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
