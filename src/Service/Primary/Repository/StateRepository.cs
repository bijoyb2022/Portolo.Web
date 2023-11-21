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
    public class StateRepository : GenericRepository<States, PrimaryContext>, IStateRepository
    {
        public StateRepository(PrimaryContext context)
            : base(context)
        {
        }

        public List<StateResponseDTO> GetState(StateRequestDTO request)
        {
            var countryKey = new SqlParameter("@CountryKey", (object)request.CountryKey ?? DBNull.Value);
            var type = new SqlParameter("@Type", (object)request.OptType ?? DBNull.Value);

            return this.dbContext.Database.SqlQuery<StateResponseDTO>("exec [Master].[upGetStates] @CountryKey,@Type",
                    countryKey,
                    type)
                .ToList();
        }

    }
}
