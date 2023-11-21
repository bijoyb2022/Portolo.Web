using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Portolo.Common.Data;
using Portolo.Common.Request;
using Portolo.Common.Response;
using Portolo.Framework.Data;
using Portolo.Services.Common.Data;

namespace Portolo.Common.Repository
{
    public class ResourceRepository : GenericRepository<LanguageResource, CommonContext>, IResourceRepository
    {
        public ResourceRepository(CommonContext context)
            : base(context)
        {
        }

        public IEnumerable<ResourceDTO> GetResources(ResourcesRequestDTO request)
        {
            var culture = new SqlParameter("@Culture", request.Culture);
            return this.dbContext
                .Database.SqlQuery<ResourceDTO>(
                    "SELECT ResourceKey,ResourceValue  FROM [Master].[LanguageResource] WHERE Culture=@Culture ",
                    new object[] { culture })
                .ToList();
        }

        public IEnumerable<LanguageResourceDTO> GetLanguageResources(LanguageResourceRequestDTO request)
        {
            var languageResourceSearchKey = new SqlParameter("@LanguageResourceSearchKey", request.LanguageResourceSearchKey);
            var culture = new SqlParameter("@Culture", request.Culture);

            return this.dbContext
                .Database.SqlQuery<LanguageResourceDTO>(
                    "Exec [master].[UpGetLanguageResources]  @LanguageResourceSearchKey, @Culture",
                    new object[] { languageResourceSearchKey, culture })
                .ToList();
        }

        public IEnumerable<LanguageResourceDTO> SaveLanguageResources(string request)
        {
            if (request != null)
            {
                var xmlLanguageResources = new SqlParameter("@XmlLanguageResources", request);

                return this.dbContext.Database.SqlQuery<LanguageResourceDTO>(
                        "Exec [master].[UpLanguageResources] @XmlLanguageResources",
                        new object[] { xmlLanguageResources })
                    .ToList();
            }

            return new List<LanguageResourceDTO>();
        }
    }
}