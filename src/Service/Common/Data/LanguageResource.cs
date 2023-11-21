using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portolo.Common.Data
{
    [Table("Master.LanguageResource")]
    public class LanguageResource
    {
        public int ResourceID { get; set; }

        public string ResourceKey { get; set; }

        public string ResourceValue { get; set; }

        public string Culture { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}