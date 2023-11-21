using Portolo.Pages.Request;
using Portolo.Pages.Response;
using System.Collections.Generic;

namespace Portolo.Pages
{
    public interface IPagesService
    {
        List<ApplicationTextResponseDTO> GetApplicationText(ApplicationTextRequestDTO request);

    }
}
