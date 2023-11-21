using Portolo.Framework.Data;
using Portolo.Pages.Data;
using Portolo.Pages.Request;
using Portolo.Pages.Response;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Portolo.Pages.Repository
{
    public class ApplicationTextRepository : GenericRepository<ApplicationText, PagesContext>, IApplicationTextRepository
    {
        public ApplicationTextRepository(PagesContext context)
            : base(context)
        {
        }

        public List<ApplicationTextResponseDTO> GetApplicationText(ApplicationTextRequestDTO request)
        {
            var applicationTextDesc = new SqlParameter("@ApplicationTextDesc", (object)request.ApplicationTextDesc ?? DBNull.Value);
            var type = new SqlParameter("@Type", (object)request.OptType ?? DBNull.Value);

            return this.dbContext.Database.SqlQuery<ApplicationTextResponseDTO>("exec [dbo].[upGetApplicationText] @ApplicationTextDesc,@Type",
                    applicationTextDesc,
                    type)
                .ToList();
        }

    }
}
