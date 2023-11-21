using Portolo.Framework.Data;
using Portolo.Framework.Security;
using Portolo.Security.Data;
using Portolo.Security.Request;
using Portolo.Security.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Portolo.Security.Repository
{
    //public class UserRepository : GenericRepository<UserLogin, SecurityContext>, IUserRepository
    public class ApplicationSettingsRepository : IApplicationSettingsRepository
    {
        string strConn = ConfigurationManager.ConnectionStrings["SqlDBCon"].ToString();
        //public UserRepository(SecurityContext context)
        //    : base(context)
        //{
        //}
    
        public List<ApplicationSettingsResponseDTO> GetApplicationSettings(ApplicationSettingsRequestDTO request)
        {
            var result = new List<ApplicationSettingsResponseDTO>();
            using (SqlConnection connection = new SqlConnection(strConn))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[upGetApplicationSettings]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@ApplicationSettingDesc", SqlDbType.VarChar).Value = request.ApplicationSettingDesc;
                    command.Parameters.Add("@Type", SqlDbType.Char).Value = "GET";
                    SqlDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            result.Add(new ApplicationSettingsResponseDTO()
                            {
                                ApplicationSettingDesc = dr["ApplicationSettingDesc"].ToString(),
                                SettingValue = dr["SettingValue"].ToString(),
                                UserEditable = Convert.ToBoolean(dr["UserEditable"].ToString()),
                                Usage = dr["Usage"].ToString(),
                                SiteName = dr["SiteName"].ToString(),
                                SLNo = dr["SLNo"].ToString(),
                                Status = dr["Status"].ToString(),
                                ApplicationSettingKey = Convert.ToInt32(dr["ApplicationSettingKey"].ToString()),
                            });
                        }
                    }
                }
                connection.Close();
            }
            return result;
        }

        public ApplicationSettingsResponseDTO GetSelectApplicationSettings(ApplicationSettingsRequestDTO request)
        {
            var result = new ApplicationSettingsResponseDTO();
            using (SqlConnection connection = new SqlConnection(strConn))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[upGetApplicationSettings]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@ApplicationSettingKey", SqlDbType.Int).Value = request.ApplicationSettingKey;
                    command.Parameters.Add("@Type", SqlDbType.Char).Value = "GET";
                    SqlDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            result.ApplicationSettingDesc = dr["ApplicationSettingDesc"].ToString();
                            result.Usage = dr["Usage"].ToString();
                            result.ApplicationSettingKey = Convert.ToInt32(dr["ApplicationSettingKey"].ToString());

                        }
                    }
                }
                connection.Close();
            }
            return result;
        }

    }
}
