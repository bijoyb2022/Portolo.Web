using System;

namespace Portolo.Common.Response
{
    public class LanguageResourceDTO
    {
        public int ResourceID { get; set; }
        public string ResourceKey { get; set; }
        public string ResourceValue { get; set; }
        public string Culture { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string LanguageDesc { get; set; }
    }
}
