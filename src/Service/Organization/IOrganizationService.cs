using Portolo.Organization.Request;
using Portolo.Organization.Response;
using System.Collections.Generic;

namespace Portolo.Organization
{
    public interface IOrganizationService
    {
        List<ApplicationTextResponseDTO> GetApplicationText(ApplicationTextRequestDTO request);

    }
}
