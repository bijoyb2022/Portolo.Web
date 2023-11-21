using Portolo.Pages.Data;
using Portolo.Pages.Request;
using Portolo.Pages.Response;
using System.Collections.Generic;
using System.Configuration;

namespace Portolo.Pages
{
    public partial class PagesService : IPagesService
    {
        public PagesService()
        {
        }
        //private string DbConnection => ConfigurationUtility.Current.GetSection<string>("DbConnection");
        private string DbConnection => ConfigurationManager.ConnectionStrings["DbConnection"].ToString();

        public List<ApplicationTextResponseDTO> GetApplicationText(ApplicationTextRequestDTO request)
        {
            using (var unitOfWork = new PagesUnitOfWork(this.DbConnection))
            {
                return unitOfWork.ApplicationTextRepository.GetApplicationText(request);
            }
        }

    }
}
