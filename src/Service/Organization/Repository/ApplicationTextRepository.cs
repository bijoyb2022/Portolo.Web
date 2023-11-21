using Portolo.Framework.Data;
using Portolo.Organization.Data;
using Portolo.Organization.Request;
using Portolo.Organization.Response;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Portolo.Organization.Repository
{
    public class ApplicationTextRepository : GenericRepository<ApplicationText, OrganizationContext>, IApplicationTextRepository
    {
        public ApplicationTextRepository(OrganizationContext context)
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
