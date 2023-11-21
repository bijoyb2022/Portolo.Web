using Portolo.Systems.Data;
using Portolo.Systems.Request;
using Portolo.Systems.Response;
using System.Collections.Generic;
using System.Configuration;

namespace Portolo.Systems
{
    public partial class SystemService : ISystemService
    {
        public SystemService()
        {
        }
        //private string DbConnection => ConfigurationUtility.Current.GetSection<string>("DbConnection");
        private string DbConnection => ConfigurationManager.ConnectionStrings["DbConnection"].ToString();

        public List<ApplicationSettingsResponseDTO> GetApplicationSettings(ApplicationSettingsRequestDTO request)
        {
            using (var unitOfWork = new SystemUnitOfWork(this.DbConnection))
            {
                return unitOfWork.ApplicationSettingsRepository.GetApplicationSettings(request);
            }
        }
        public ApplicationSettingsResponseDTO GetSelectApplicationSettings(ApplicationSettingsRequestDTO request)
        {
            using (var unitOfWork = new SystemUnitOfWork(this.DbConnection))
            {
                return unitOfWork.ApplicationSettingsRepository.GetSelectApplicationSettings(request);
            }
        }
        public List<ApplicationTextResponseDTO> GetApplicationText(ApplicationTextRequestDTO request)
        {
            using (var unitOfWork = new SystemUnitOfWork(this.DbConnection))
            {
                return unitOfWork.ApplicationTextRepository.GetApplicationText(request);
            }
        }

    }
}
