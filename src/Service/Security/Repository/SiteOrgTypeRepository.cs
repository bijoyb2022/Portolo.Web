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
    public class SiteOrgTypeRepository : ISiteOrgTypeRepository
    {
        string strConn = ConfigurationManager.ConnectionStrings["SqlDBCon"].ToString();
        //public UserRepository(SecurityContext context)
        //    : base(context)
        //{
        //}

        public List<SiteOrgTypeResponseDTO> GetSiteOrgType(SiteOrgTypeRequestDTO request)
        {
            var result = new List<SiteOrgTypeResponseDTO>();
            using (SqlConnection connection = new SqlConnection(strConn))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("[Master].[upGetSiteOrgType]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@OrganizationTypeKey", SqlDbType.Int).Value = request.OrganizationTypeKey;
                    command.Parameters.Add("@Type", SqlDbType.Char).Value = "GET";
                    SqlDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            result.Add(new SiteOrgTypeResponseDTO()
                            {
                                SiteOrgTypeName = dr["OrganizationTypeDesc"].ToString(),
                                PresentationOrder = Convert.ToInt32(dr["PresentationOrder"].ToString()),
                                OrganizationTypeKey = Convert.ToInt32(dr["OrganizationTypeKey"].ToString()),
                                SiteOrgTypeKey = Convert.ToInt32(dr["SiteOrgTypeKey"].ToString()),
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
