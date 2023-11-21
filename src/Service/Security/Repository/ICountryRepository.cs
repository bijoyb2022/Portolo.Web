using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Portolo.Framework.Data;
using Portolo.Security.Data;
using Portolo.Security.Request;
using Portolo.Security.Response;

namespace Portolo.Security.Repository
{
    public interface ICountryRepository //: IGenericRepository<UserLogin, SecurityContext>
    {
        List<CountryResponseDTO> GetCountry(CountryRequestDTO request);
        CountryResponseDTO GetSelectCountry(CountryRequestDTO request);
        int SaveCountry(CountryRequestDTO request);
    }
}
