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
    public class CountryTimeZoneRepository : GenericRepository<CountryTimeZone, PrimaryContext>, ICountryTimeZoneRepository
    {
        public CountryTimeZoneRepository(PrimaryContext context)
            : base(context)
        {
        }

        public List<CountryTimeZoneResponseDTO> GetCountryTimeZone(CountryTimeZoneRequestDTO request)
        {
            var countryKey = new SqlParameter("@CountryKey", (object)request.CountryKey ?? DBNull.Value);
            var type = new SqlParameter("@Type", (object)request.OptType ?? DBNull.Value);

            return this.dbContext.Database.SqlQuery<CountryTimeZoneResponseDTO>("exec [Master].[upGetCountryTimeZone] @CountryKey,@Type",
                    countryKey,
                    type)
                .ToList();
        }
    }
}
