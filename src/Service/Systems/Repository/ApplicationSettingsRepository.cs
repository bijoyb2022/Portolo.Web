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
    public class ApplicationSettingsRepository : GenericRepository<ApplicationSettings, SystemContext>, IApplicationSettingsRepository
    {
        public ApplicationSettingsRepository(SystemContext context)
            : base(context)
        {
        }

        public List<ApplicationSettingsResponseDTO> GetApplicationSettings(ApplicationSettingsRequestDTO request)
        {
            var applicationSettingKey = new SqlParameter("@ApplicationSettingKey", (object)request.ApplicationSettingKey ?? DBNull.Value);
            var applicationSettingDesc = new SqlParameter("@ApplicationSettingDesc", (object)request.ApplicationSettingDesc ?? DBNull.Value);
            var type = new SqlParameter("@Type", (object)request.OptType ?? DBNull.Value);

            return this.dbContext.Database.SqlQuery<ApplicationSettingsResponseDTO>("exec [dbo].[upGetApplicationSettings] @ApplicationSettingKey,@ApplicationSettingDesc,@Type",
                   applicationSettingKey,
                   applicationSettingDesc,
                    type)
                .ToList();
        }

        public ApplicationSettingsResponseDTO GetSelectApplicationSettings(ApplicationSettingsRequestDTO request)
        {
            var applicationSettingKey = new SqlParameter("@ApplicationSettingKey", (object)request.ApplicationSettingKey ?? DBNull.Value);
            var applicationSettingDesc = new SqlParameter("@ApplicationSettingDesc", (object)request.ApplicationSettingDesc ?? DBNull.Value);
            var type = new SqlParameter("@Type", (object)request.OptType ?? DBNull.Value);

            return this.dbContext.Database.SqlQuery<ApplicationSettingsResponseDTO>("exec [dbo].[upGetApplicationSettings] @ApplicationSettingKey,@ApplicationSettingDesc,@Type",
                   applicationSettingKey,
                   applicationSettingDesc,
                    type)
                .FirstOrDefault();
        }

    }
}
