using Portolo.Organization.Data;
using Portolo.Organization.Request;
using Portolo.Organization.Response;
using System.Collections.Generic;
using System.Configuration;

namespace Portolo.Organization
{
    public partial class OrganizationService : IOrganizationService
    {
        public OrganizationService()
        {
        }
        //private string DbConnection => ConfigurationUtility.Current.GetSection<string>("DbConnection");
        private string DbConnection => ConfigurationManager.ConnectionStrings["DbConnection"].ToString();
             
        public List<ApplicationTextResponseDTO> GetApplicationText(ApplicationTextRequestDTO request)
        {
            using (var unitOfWork = new OrganizationUnitOfWork(this.DbConnection))
            {
                return unitOfWork.ApplicationTextRepository.GetApplicationText(request);
            }
        }

    }
}
