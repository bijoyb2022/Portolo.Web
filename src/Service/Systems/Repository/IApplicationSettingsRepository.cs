using Portolo.Framework.Data;
using Portolo.Systems.Data;
using Portolo.Systems.Request;
using Portolo.Systems.Response;
using System.Collections.Generic;

namespace Portolo.Systems.Repository
{
    public interface IApplicationSettingsRepository : IGenericRepository<ApplicationSettings, SystemContext>
    {
        List<ApplicationSettingsResponseDTO> GetApplicationSettings(ApplicationSettingsRequestDTO request);
        ApplicationSettingsResponseDTO GetSelectApplicationSettings(ApplicationSettingsRequestDTO request);
    }
}
