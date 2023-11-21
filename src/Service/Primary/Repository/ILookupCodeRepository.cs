using Portolo.Framework.Data;
using Portolo.Primary.Data;
using Portolo.Primary.Request;
using Portolo.Primary.Response;
using System.Collections.Generic;

namespace Portolo.Primary.Repository
{
    public interface ILookupCodeRepository : IGenericRepository<LookupCode, PrimaryContext>
    {
        List<LookupCodeResponseDTO> GetLookupCode(LookupCodeRequestDTO request);
        LookupCodeResponseDTO GetSelectLookupCode(LookupCodeRequestDTO request);
        int SaveLookupCode(LookupCodeRequestDTO request);
    }
}
