using Portolo.Framework.Data;
using Portolo.Systems.Data;
using Portolo.Systems.Request;
using Portolo.Systems.Response;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Portolo.Systems.Repository
{
    public class ApplicationTextRepository : GenericRepository<ApplicationText, SystemContext>, IApplicationTextRepository
    {
        public ApplicationTextRepository(SystemContext context)
            : base(context)
        {
        }

        public List<ApplicationTextResponseDTO> GetApplicationText(ApplicationTextRequestDTO request)
        {
            var applicationTextKey = new SqlParameter("@ApplicationTextKey", (object)request.ApplicationTextKey ?? DBNull.Value);
            var applicationTextDesc = new SqlParameter("@ApplicationTextDesc", (object)request.ApplicationTextDesc ?? DBNull.Value);
            var type = new SqlParameter("@Type", (object)request.OptType ?? DBNull.Value);

            return this.dbContext.Database.SqlQuery<ApplicationTextResponseDTO>("exec [dbo].[upGetApplicationText] @ApplicationTextKey,@ApplicationTextDesc,@Type",
                    applicationTextKey,
                    applicationTextDesc,
                    type)
                .ToList();
        }

    }
}
