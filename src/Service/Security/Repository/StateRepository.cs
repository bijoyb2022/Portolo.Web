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
    public class StateRepository : IStateRepository
    {
        string strConn = ConfigurationManager.ConnectionStrings["SqlDBCon"].ToString();
        //public UserRepository(SecurityContext context)
        //    : base(context)
        //{
        //}
    
        public List<StateResponseDTO> GetState(StateRequestDTO request)
        {
            var result = new List<StateResponseDTO>();
            using (SqlConnection connection = new SqlConnection(strConn))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("[Master].[upGetStates]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@CountryKey", SqlDbType.Int).Value = request.CountryKey;
                    command.Parameters.Add("@Type", SqlDbType.Char).Value = "GET";
                    SqlDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            result.Add(new StateResponseDTO()
                            {
                                StateCode = dr["StateCode"].ToString(),
                                StateName = dr["StateName"].ToString(),
                                StateKey = Convert.ToInt32(dr["StateKey"].ToString()),
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
