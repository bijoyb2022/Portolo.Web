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
    public class LookupCodeRepository : GenericRepository<LookupCode, PrimaryContext>, ILookupCodeRepository
    {
        public LookupCodeRepository(PrimaryContext context)
            : base(context)
        {
        }

        public List<LookupCodeResponseDTO> GetLookupCode(LookupCodeRequestDTO request)
        {
            var lookupCodeKey = new SqlParameter("@LookupCodeKey", (object)request.LookupCodeKey ?? DBNull.Value);
            var lookupCodeType = new SqlParameter("@LookupCodeType", (object)request.LookupCodeType ?? DBNull.Value);
            var type = new SqlParameter("@Type", (object)request.OptType ?? DBNull.Value);

            return this.dbContext.Database.SqlQuery<LookupCodeResponseDTO>("exec [dbo].[upGetLookupCode] @LookupCodeKey,@LookupCodeType,@Type",
                    lookupCodeKey,
                    lookupCodeType,
                    type)
                .ToList();
        }

        public LookupCodeResponseDTO GetSelectLookupCode(LookupCodeRequestDTO request)
        {
            var lookupCodeKey = new SqlParameter("@LookupCodeKey", (object)request.LookupCodeKey ?? DBNull.Value);
            var type = new SqlParameter("@Type", (object)request.OptType ?? DBNull.Value);

            return this.dbContext.Database.SqlQuery<LookupCodeResponseDTO>("exec [dbo].[upGetLookupCode] @LookupCodeKey,@Type",
                    lookupCodeKey,
                    type)
                .FirstOrDefault();
        }

        public int SaveLookupCode(LookupCodeRequestDTO request)
        {
            var lookupCodeKey = new SqlParameter("@LookupCodeKey", request.LookupCodeKey);
            var lookupCodeType = new SqlParameter("@LookupCodeType", request.LookupCodeType);
            var codeDesc = new SqlParameter("@CodeDesc", request.CodeDesc);
            var displayCodeDesc = new SqlParameter("@DisplayCodeDesc", request.DisplayCodeDesc);
            var type = new SqlParameter("@Type", request.OptType);
            var indicator = new SqlParameter("@OutputStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };

            this.dbContext.Database.SqlQuery<LookupCodeResponseDTO>("exec [dbo].[upSaveLookupCode] @LookupCodeKey,@LookupCodeType,@CodeDesc,@DisplayCodeDesc,@Type,@OutputStatus output",
                lookupCodeKey, lookupCodeType, codeDesc, displayCodeDesc, type, indicator);

            return Convert.ToInt32(indicator);
        }

    }
}
