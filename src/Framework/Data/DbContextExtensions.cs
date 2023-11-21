using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Portolo.Framework.Data
{
    public static class DbContextExtensions
    {
        public static async Task<SqlConnection> GetOpenSqlConnectionAsync(this DbContext context)
        {
            var conn = new SqlConnection(context.Database.Connection.ConnectionString);

            if (conn.State != ConnectionState.Open)
            {
                await conn.OpenAsync().ConfigureAwait(false);
            }

            return conn;
        }
    }
}
