using System;
using System.Collections.Generic;

namespace Portolo.Security.Response
{
    public class SamlSSOConfigDTO
    {
        public int ID { get; set; }
        public string Issuer { get; set; }
        public string IdPCertificate { get; set; }
        public string IdPIssuerNameIdentifier { get; set; }
        public string IdPAssertionUserNamePath { get; set; }
        public string IdPSSODefaultPassword { get; set; }
        public string SPIssuerNameIdentifier { get; set; }
        public string SPRedirectUrl { get; set; }
        public string SPNameID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
