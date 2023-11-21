using Portolo.Systems.Request;
using Portolo.Systems.Response;
using System.Collections.Generic;

namespace Portolo.Systems
{
    public interface ISystemService
    {
        List<ApplicationSettingsResponseDTO> GetApplicationSettings(ApplicationSettingsRequestDTO request);
        ApplicationSettingsResponseDTO GetSelectApplicationSettings(ApplicationSettingsRequestDTO request);

        List<ApplicationTextResponseDTO> GetApplicationText(ApplicationTextRequestDTO request);

    }
}
