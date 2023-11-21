using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Protolo.Application.Web.Areas.System.Models
{
    //public class LookupViewModel
    //{
    //    public int? LookupCodeKey { get; set; }
    //    public string CodeId { get; set; }
    //    public string CodeDesc { get; set; }
    //    public string DisplayCodeDesc { get; set; }
    //    public int? PresentationOrder { get; set; }
    //    public string LookupCodeType { get; set; }
    //    public string Status { get; set; }
    //    public int? CreatedBy { get; set; }
    //    public DateTime CreatedOn { get; set; }
    //    public int? ModifiedBy { get; set; }
    //    public DateTime? ModifiedOn { get; set; }
    //    public List<SelectListItem> LookupCodeTypeList { get; set; }
    //}

    public class LookupCodeViewModel
    {
        public List<SelectListItem> LookupCodeTypeList { get; set; }
        public List<LookupCodeModel> LookupCodeList { get; set; }

        // Additional properties and methods can be added as needed
        public int? LookupCodeKey { get; set; }
    }

    public class LookupCodeModel
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