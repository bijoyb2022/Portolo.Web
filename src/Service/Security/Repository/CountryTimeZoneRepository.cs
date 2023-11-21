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
    public class CountryTimeZoneRepository : ICountryTimeZoneRepository
    {
        string strConn = ConfigurationManager.ConnectionStrings["SqlDBCon"].ToString();
        //public UserRepository(SecurityContext context)
        //    : base(context)
        //{
        //}
    
        public List<CountryTimeZoneResponseDTO> GetCountryTimeZone(CountryTimeZoneRequestDTO request)
        {
            var result = new List<CountryTimeZoneResponseDTO>();
            using (SqlConnection connection = new SqlConnection(strConn))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("[Master].[upGetCountryTimeZone]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@CountryKey", SqlDbType.Int).Value = request.CountryKey;
                    command.Parameters.Add("@Type", SqlDbType.Char).Value = "GET";
                    SqlDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            result.Add(new CountryTimeZoneResponseDTO()
                            {
                                CountryCode = dr["CountryCode"].ToString(),
                                Coordinates = dr["Coordinates"].ToString(),
                                TimeZone = dr["TimeZone"].ToString(),
                                //Format = dr["Format"].ToString(),
                                //UTCoffset = Convert.ToDecimal(dr["UTCoffset"].ToString()),
                                //UTCDSToffset = dr["UTCDSToffset"].ToString(),
                                //IsInternational = Convert.ToInt32(dr["IsInternational"].ToString()),
                                //TimeZoneID = dr["TimeZoneID"].ToString(),
                                //TimeZoneOld = dr["TimeZoneOld"].ToString(),
                                CountryTimeZoneKey = Convert.ToInt32(dr["CountryTimeZoneKey"].ToString()),
                            });
                        }
                    }
                }
                connection.Close();
            }
            return result;
        }

        //public CountryResponseDTO GetSelectCountry(CountryRequestDTO request)
        //{
        //    var result = new CountryResponseDTO();
        //    using (SqlConnection connection = new SqlConnection(strConn))
        //    {
        //        connection.Open();
        //        using (SqlCommand command = new SqlCommand("[Master].[upGetCountry]", connection))
        //        {
        //            command.CommandType = CommandType.StoredProcedure;
        //            command.Parameters.Add("@CountryKey", SqlDbType.Int).Value = request.CountryKey;
        //            command.Parameters.Add("@Type", SqlDbType.Char).Value = "GET";
        //            SqlDataReader dr = command.ExecuteReader();
        //            if (dr.HasRows)
        //            {
        //                while (dr.Read())
        //                {
        //                    result.CountryName = dr["CountryName"].ToString();
        //                    result.CountryKey = Convert.ToInt32(dr["CountryKey"].ToString());

        //                }
        //            }
        //        }
        //        connection.Close();
        //    }
        //    return result;
        //}

        //public int SaveCountry(CountryRequestDTO request)
        //{
        //    int indicator = 0;
        //    var categoryCode = string.Empty;
        //    using (SqlConnection connection = new SqlConnection(strConn))
        //    {
        //        connection.Open();
        //        using (SqlCommand command = new SqlCommand("[Master].[upSaveCountry]", connection))
        //        {
        //            command.CommandType = CommandType.StoredProcedure;
        //            command.Parameters.Add("@CountryKey", SqlDbType.Int).Value = request.CountryKey;
        //            command.Parameters.Add("@CountryName", SqlDbType.VarChar).Value = request.CountryName;
        //            command.Parameters.Add("@Type", SqlDbType.Char).Value = request.OptType;
        //            command.Parameters.Add("@OutputStatus", SqlDbType.Int).Direction = ParameterDirection.Output;
        //            command.ExecuteNonQuery();
        //            indicator = Convert.ToInt32(command.Parameters["@OutputStatus"].Value);
        //        }
        //        connection.Close();
        //    }
        //    return indicator;
        //}
    }
}
