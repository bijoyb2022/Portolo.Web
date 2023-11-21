using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Portolo.Common.Data;
using Portolo.Common.Repository;
using Portolo.Common.Request;
using Portolo.Common.Response;
using Portolo.Framework.Utils;
using Portolo.Utility.Configuration;

namespace Portolo.Common
{
    public class CommonService : ICommonService
    {
        private static readonly IMapper CommonServiceMapper;

        static CommonService()
        {
            CommonServiceMapper = new MapperConfiguration(c =>
            {
                c.CreateMissingTypeMaps = false;
                c.AddProfile<CommonServiceProfile>();
            }).CreateMapper();
        }

        private string DbConnection => ConfigurationUtility.Current.GetSection<string>("DbConnection").DefaultIfNull(string.Empty);

        public List<ResourceDTO> GetResources(ResourcesRequestDTO request)
        {
            using (var unitOfWork = new CommonUnitOfWork(this.DbConnection))
            {
                return unitOfWork.ResourceRepository.GetResources(request).ToList();
            }
        }

        public IEnumerable<LanguageDTO> GetLanguages(LanguagesRequestDTO request)
        {
            using (var unitOfWork = new CommonUnitOfWork(this.DbConnection))
            {
                var displayLanguage = "Y";
                var languages = unitOfWork.LanguageRepository.GetLanguages(displayLanguage);
                return CommonServiceMapper.Map<IEnumerable<Language>, IEnumerable<LanguageDTO>>(languages.AsEnumerable());
            }
        }

        public IEnumerable<LanguageResourceDTO> GetLanguageResources(LanguageResourceRequestDTO request)
        {
            if (request != null)
            {
                request.Culture = string.IsNullOrEmpty(request.Culture) ? string.Empty : request.Culture.Trim();
                request.LanguageResourceSearchKey = string.IsNullOrEmpty(request.LanguageResourceSearchKey)
                    ? string.Empty
                    : request.LanguageResourceSearchKey.Trim();
            }

            using (var unitOfWork = new CommonUnitOfWork(this.DbConnection))
            {
                return unitOfWork.ResourceRepository.GetLanguageResources(request);
            }
        }

    }
}