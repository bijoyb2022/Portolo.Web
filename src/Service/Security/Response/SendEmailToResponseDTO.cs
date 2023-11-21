using System;

namespace Portolo.Security.Response
{
    public class SendEmailToResponseDTO
    {
        public int? SendEmailToKey { get; set; }
        public string SendEmailToDesc { get; set; }
        public int? PresentationOrder { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

    }
}
