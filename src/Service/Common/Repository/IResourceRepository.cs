using System.Collections.Generic;
using Portolo.Common.Data;
using Portolo.Common.Request;
using Portolo.Common.Response;
using Portolo.Framework.Data;
using Portolo.Services.Common.Data;

namespace Portolo.Common.Repository
{
    public interface IResourceRepository : IGenericRepository<LanguageResource, CommonContext>
    {
        IEnumerable<ResourceDTO> GetResources(ResourcesRequestDTO request);
        IEnumerable<LanguageResourceDTO> SaveLanguageResources(string request);
        IEnumerable<LanguageResourceDTO> GetLanguageResources(LanguageResourceRequestDTO request);
    }
}