using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Portolo.Framework.Data;
using Portolo.Security.Data;
using Portolo.Security.Request;
using Portolo.Security.Response;

namespace Portolo.Security.Repository
{
    public interface ISuffixesRepository //: IGenericRepository<UserLogin, SecurityContext>
    {
        List<SuffixesResponseDTO> GetSuffixes();
    }
}
