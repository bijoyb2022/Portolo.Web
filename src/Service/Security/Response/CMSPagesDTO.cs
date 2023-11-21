using System;

namespace Portolo.Security.Response
{
    public class CMSPagesDTO
    {
        public int PageID { get; set; }
        public int ApplicationID { get; set; }
        public string PageName { get; set; }
        public string PageMetaTag { get; set; }
        public string PageContent { get; set; }
        public string Active { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
