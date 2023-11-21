using Portolo.Framework.Data;
using Portolo.Primary.Data;
using Portolo.Primary.Request;
using Portolo.Primary.Response;
using System.Collections.Generic;

namespace Portolo.Primary.Repository
{
    public interface ICountryRepository : IGenericRepository<Countries, PrimaryContext>
    {
        List<CountryResponseDTO> GetCountry(CountryRequestDTO request);
        CountryResponseDTO GetSelectCountry(CountryRequestDTO request);
        int SaveCountry(CountryRequestDTO request);
    }
}
