using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portolo.Common.Data
{
    [Table("Master.Languages")]
    public class Language
    {
        public int LanguageID { get; set; }

        public string LanguageCode { get; set; }

        public string LanguageDesc { get; set; }

        public int DisplayOrder { get; set; }

        public string DisplayLanguage { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}