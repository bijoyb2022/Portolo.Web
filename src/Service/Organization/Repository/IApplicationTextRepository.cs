using Portolo.Framework.Data;
using Portolo.Organization.Data;
using Portolo.Organization.Request;
using Portolo.Organization.Response;
using System.Collections.Generic;

namespace Portolo.Organization.Repository
{
    public interface IApplicationTextRepository : IGenericRepository<ApplicationText, OrganizationContext>
    {
        List<ApplicationTextResponseDTO> GetApplicationText(ApplicationTextRequestDTO request);
    }
}
