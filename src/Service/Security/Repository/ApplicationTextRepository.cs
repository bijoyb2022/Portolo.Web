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
    public class ApplicationTextRepository : IApplicationTextRepository
    {
        string strConn = ConfigurationManager.ConnectionStrings["SqlDBCon"].ToString();
        //public UserRepository(SecurityContext context)
        //    : base(context)
        //{
        //}

        public List<ApplicationTextResponseDTO> GetApplicationText(ApplicationTextRequestDTO request)
        {
            var result = new List<ApplicationTextResponseDTO>();
            using (SqlConnection connection = new SqlConnection(strConn))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[upGetApplicationText]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@ApplicationTextDesc", SqlDbType.VarChar).Value = request.ApplicationTextDesc;
                    command.Parameters.Add("@Type", SqlDbType.Char).Value = "GET";
                    SqlDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            result.Add(new ApplicationTextResponseDTO()
                            {
                                ApplicationTextDesc = dr["ApplicationTextDesc"].ToString(),
                                SettingValue = dr["SettingValue"].ToString(),
                                UserEditable = Convert.ToBoolean(dr["UserEditable"].ToString()),
                                SiteName = dr["SiteName"].ToString(),
                                SLNo = dr["SLNo"].ToString(),
                                Status = dr["Status"].ToString(),
                                ApplicationTextKey = Convert.ToInt32(dr["ApplicationTextKey"].ToString()),
                            });
                        }
                    }
                }
                connection.Close();
            }
            return result;
        }

    }
}
