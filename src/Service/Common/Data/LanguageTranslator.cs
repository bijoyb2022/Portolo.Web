using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portolo.Common.Data
{
    [Table("Master.LanguageTranslator")]
    public class LanguageTranslator
    {
        public int TranslatorId { get; set; }

        public string LabelName { get; set; }

        [Column("en-us", TypeName = "ntext")]
        public string en_us { get; set; }

        [Column("es-cr", TypeName = "ntext")]
        public string es_cr { get; set; }

        [Column("it-it", TypeName = "ntext")]
        public string it_it { get; set; }

        [Column("zh-cn", TypeName = "ntext")]
        public string zh_cn { get; set; }

        [Column("zh-tw", TypeName = "ntext")]
        public string zh_tw { get; set; }

        [Column("fr-fr", TypeName = "ntext")]
        public string fr_fr { get; set; }

        [Column("nl-nl", TypeName = "ntext")]
        public string nl_nl { get; set; }

        [Column("pt-pt", TypeName = "ntext")]
        public string pt_pt { get; set; }

        [Column("de-de", TypeName = "ntext")]
        public string de_de { get; set; }

        [Column("es-es", TypeName = "ntext")]
        public string es_es { get; set; }

        [Column("pt-br", TypeName = "ntext")]
        public string pt_br { get; set; }

        [Column("es-mx", TypeName = "ntext")]
        public string es_mx { get; set; }

        [Column("pl-pl", TypeName = "ntext")]
        public string pl_pl { get; set; }

        [Column("fi-fi", TypeName = "ntext")]
        public string fi_fi { get; set; }

        [Column("ko-kr", TypeName = "ntext")]
        public string ko_kr { get; set; }

        [Column("ja-jp", TypeName = "ntext")]
        public string ja_jp { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}