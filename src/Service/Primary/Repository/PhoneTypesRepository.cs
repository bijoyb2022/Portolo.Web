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
    public class PhoneTypesRepository : GenericRepository<PhoneType, PrimaryContext>, IPhoneTypesRepository
    {
        public PhoneTypesRepository(PrimaryContext context)
            : base(context)
        {
        }

        public List<PhoneTypesResponseDTO> GetPhoneTypes(PhoneTypesRequestDTO request)
        {
            var type = new SqlParameter("@Type", (object)request.OptType ?? DBNull.Value);

            return this.dbContext.Database.SqlQuery<PhoneTypesResponseDTO>("exec [Master].[upGetPhoneTypes] @Type",                    
                    type)
                .ToList();
        }

    }
}
