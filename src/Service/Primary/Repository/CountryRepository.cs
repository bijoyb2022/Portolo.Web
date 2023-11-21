using Portolo.Framework.Data;
using Portolo.Primary.Data;
using Portolo.Primary.Request;
using Portolo.Primary.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Portolo.Primary.Repository
{
    public class CountryRepository : GenericRepository<Countries, PrimaryContext>, ICountryRepository
    {
        public CountryRepository(PrimaryContext context)
            : base(context)
        {
        }

        public List<CountryResponseDTO> GetCountry(CountryRequestDTO request)
        {
            var countryKey = new SqlParameter("@CountryKey", (object)request.CountryKey ?? DBNull.Value);
            var type = new SqlParameter("@Type", (object)request.OptType ?? DBNull.Value);

            return this.dbContext.Database.SqlQuery<CountryResponseDTO>("exec [Master].[upGetCountries] @CountryKey,@Type",
                    countryKey,
                    type)
                .ToList();           
        }

        public CountryResponseDTO GetSelectCountry(CountryRequestDTO request)
        {
            var countryKey = new SqlParameter("@CountryKey", (object)request.CountryKey ?? DBNull.Value);
            var type = new SqlParameter("@Type", (object)request.OptType ?? DBNull.Value);

            return this.dbContext.Database.SqlQuery<CountryResponseDTO>("exec [Master].[upGetCountries] @CountryKey,@Type",
                    countryKey,
                    type)
                .FirstOrDefault();

        }

        public int SaveCountry(CountryRequestDTO request)
        {
            var countryKey = new SqlParameter("@CountryKey", request.CountryKey);
            var countryName = new SqlParameter("@CountryName", request.CountryName);
            var type = new SqlParameter("@Type", request.OptType);
            var indicator = new SqlParameter("@OutputStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };

            this.dbContext.Database.SqlQuery<CountryResponseDTO>("exec [Master].[upSaveCountry] @CountryKey,@CountryName,@Type,@OutputStatus output",
                countryKey, countryName, type, indicator);

            return Convert.ToInt32(indicator);
        }

    }
}
