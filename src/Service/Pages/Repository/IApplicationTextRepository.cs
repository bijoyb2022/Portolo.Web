using Portolo.Framework.Data;
using Portolo.Pages.Data;
using Portolo.Pages.Request;
using Portolo.Pages.Response;
using System.Collections.Generic;

namespace Portolo.Pages.Repository
{
    public interface IApplicationTextRepository : IGenericRepository<ApplicationText, PagesContext>
    {
        List<ApplicationTextResponseDTO> GetApplicationText(ApplicationTextRequestDTO request);
    }
}
