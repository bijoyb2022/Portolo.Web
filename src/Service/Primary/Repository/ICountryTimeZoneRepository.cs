using Portolo.Framework.Data;
using Portolo.Primary.Data;
using Portolo.Primary.Request;
using Portolo.Primary.Response;
using System.Collections.Generic;

namespace Portolo.Primary.Repository
{
    public interface ICountryTimeZoneRepository : IGenericRepository<CountryTimeZone, PrimaryContext>
    {
        List<CountryTimeZoneResponseDTO> GetCountryTimeZone(CountryTimeZoneRequestDTO request);
    }
}
