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
using System.IdentityModel.Protocols.WSTrust;

namespace Portolo.Security.Repository
{
    //public class UserRepository : GenericRepository<UserLogin, SecurityContext>, IUserRepository
    public class LookupCodeRepository : ILookupCodeRepository
    {
        string strConn = ConfigurationManager.ConnectionStrings["SqlDBCon"].ToString();
        //public UserRepository(SecurityContext context)
        //    : base(context)
        //{
        //}

        public List<LookupCodeResponseDTO> GetLookupCode(LookupCodeRequestDTO request)
        {
            var result = new List<LookupCodeResponseDTO>();
            using (SqlConnection connection = new SqlConnection(strConn))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[upGetLookupCode]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@LookupCodeKey", SqlDbType.Int).Value = request.LookupCodeKey > 0 ? request.LookupCodeKey : null;
                    command.Parameters.Add("@LookupCodeType", SqlDbType.VarChar).Value = request.LookupCodeType;
                    command.Parameters.Add("@Type", SqlDbType.Char).Value = request.OptType;
                    SqlDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            if (request.OptType == "ALL")
                            {
                                result.Add(new LookupCodeResponseDTO()
                                {
                                    LookupCodeType = dr["LookupCodeType"].ToString(),
                                    Status = dr["Status"].ToString(),
                                    LookupCodeKey = Convert.ToInt32(dr["LookupCodeKey"].ToString()),
                                });
                            }
                            else
                            {
                                result.Add(new LookupCodeResponseDTO()
                                {
                                    CodeId = dr["CodeId"].ToString(),
                                    CodeDesc = dr["CodeDesc"].ToString(),
                                    DisplayCodeDesc = dr["DisplayCodeDesc"].ToString(),
                                    PresentationOrder = Convert.ToInt32(dr["PresentationOrder"].ToString()),
                                    LookupCodeType = dr["LookupCodeType"].ToString(),
                                    Status = dr["Status"].ToString(),
                                    LookupCodeKey = Convert.ToInt32(dr["LookupCodeKey"].ToString()),
                                });
                            }
                        }
                    }
                }
                connection.Close();
            }
            return result;
        }

        public LookupCodeResponseDTO GetSelectLookupCode(LookupCodeRequestDTO request)
        {
            var result = new LookupCodeResponseDTO();
            using (SqlConnection connection = new SqlConnection(strConn))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("[LookUp].[upGetLookupCode]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@LookupCodeKey", SqlDbType.Int).Value = request.LookupCodeKey;
                    command.Parameters.Add("@Type", SqlDbType.Char).Value = "GET";
                    SqlDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            result.CodeId = dr["CodeId"].ToString();
                            result.CodeDesc = dr["CodeDesc"].ToString();
                            result.DisplayCodeDesc = dr["DisplayCodeDesc"].ToString();
                            result.PresentationOrder = Convert.ToInt32(dr["PresentationOrder"].ToString());
                            result.LookupCodeType = dr["LookupCodeType"].ToString();
                            result.Status = dr["Status"].ToString();
                            result.LookupCodeKey = Convert.ToInt32(dr["LookupCodeKey"].ToString());

                        }
                    }
                }
                connection.Close();
            }
            return result;
        }

        public int SaveLookupCode(LookupCodeRequestDTO request)
        {
            int indicator = 0;
            var categoryCode = string.Empty;
            using (SqlConnection connection = new SqlConnection(strConn))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("[Lookup].[upSaveLookupCode]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@LookupCodeKey", SqlDbType.Int).Value = request.LookupCodeKey;
                    command.Parameters.Add("@LookupCodeType", SqlDbType.VarChar).Value = request.LookupCodeType;
                    command.Parameters.Add("@CodeDesc", SqlDbType.VarChar).Value = request.CodeDesc;
                    command.Parameters.Add("@DisplayCodeDesc", SqlDbType.VarChar).Value = request.DisplayCodeDesc;
                    command.Parameters.Add("@Type", SqlDbType.Char).Value = request.OptType;
                    command.Parameters.Add("@OutputStatus", SqlDbType.Int).Direction = ParameterDirection.Output;
                    command.ExecuteNonQuery();
                    indicator = Convert.ToInt32(command.Parameters["@OutputStatus"].Value);
                }
                connection.Close();
            }
            return indicator;
        }

    }
}
