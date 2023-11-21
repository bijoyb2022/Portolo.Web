using Portolo.Framework.Data;
using Portolo.Primary.Data;
using Portolo.Primary.Request;
using Portolo.Primary.Response;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Portolo.Primary.Repository
{
    public class OrganizationTypesRepository : GenericRepository<OrganizationType, PrimaryContext>, IOrganizationTypesRepository
    {
        public OrganizationTypesRepository(PrimaryContext context)
            : base(context)
        {
        }

        public List<OrganizationTypesResponseDTO> GetOrganizationTypes(OrganizationTypesRequestDTO request)
        {
            var type = new SqlParameter("@Type", (object)request.OptType ?? DBNull.Value);

            return this.dbContext.Database.SqlQuery<OrganizationTypesResponseDTO>("exec [Master].[upGetOrganizationTypes] @Type",
                    type)
                .ToList();          
        }

    }
}
