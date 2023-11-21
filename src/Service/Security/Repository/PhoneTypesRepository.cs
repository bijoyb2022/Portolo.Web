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
    public class PhoneTypesRepository : IPhoneTypesRepository
    {
        string strConn = ConfigurationManager.ConnectionStrings["SqlDBCon"].ToString();
        //public UserRepository(SecurityContext context)
        //    : base(context)
        //{
        //}
    
        public List<PhoneTypesResponseDTO> GetPhoneTypes()
        {
            var result = new List<PhoneTypesResponseDTO>();
            using (SqlConnection connection = new SqlConnection(strConn))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("[Master].[upGetPhoneTypes]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Type", SqlDbType.Char).Value = "GET";
                    SqlDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            result.Add(new PhoneTypesResponseDTO()
                            {
                                PhoneTypeDesc = dr["PhoneTypeDesc"].ToString(),
                                PresentationOrder = Convert.ToInt32(dr["PresentationOrder"].ToString()),
                                SCOrder = Convert.ToInt32(dr["SCOrder"].ToString()),
                                PhoneTypeKey = Convert.ToInt32(dr["PhoneTypeKey"].ToString()),
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
