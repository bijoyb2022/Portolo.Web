using Portolo.Framework.Data;
using Portolo.Primary.Data;
using Portolo.Primary.Request;
using Portolo.Primary.Response;
using System.Collections.Generic;

namespace Portolo.Primary.Repository
{
    public interface IPhoneTypesRepository : IGenericRepository<PhoneType, PrimaryContext>
    {
        List<PhoneTypesResponseDTO> GetPhoneTypes(PhoneTypesRequestDTO request);
    }
}
