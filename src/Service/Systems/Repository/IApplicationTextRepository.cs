using Portolo.Framework.Data;
using Portolo.Systems.Data;
using Portolo.Systems.Request;
using Portolo.Systems.Response;
using System.Collections.Generic;

namespace Portolo.Systems.Repository
{
    public interface IApplicationTextRepository : IGenericRepository<ApplicationText, SystemContext>
    {
        List<ApplicationTextResponseDTO> GetApplicationText(ApplicationTextRequestDTO request);
    }
}
