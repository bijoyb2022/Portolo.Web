using System.Collections.Generic;
using Portolo.Common.Request;
using Portolo.Common.Response;

namespace Portolo.Common
{
    public interface ICommonService
    {
        List<ResourceDTO> GetResources(ResourcesRequestDTO request);
        IEnumerable<LanguageDTO> GetLanguages(LanguagesRequestDTO request);
        IEnumerable<LanguageResourceDTO> GetLanguageResources(LanguageResourceRequestDTO request);
    }
}
