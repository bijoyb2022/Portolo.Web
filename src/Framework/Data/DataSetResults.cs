using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Portolo.Framework.Utils;

namespace Portolo.Framework.Data
{
    public static class DataSetResults
    {
        public static ExecuteDataSetWrapper DataSet(this DbContext dbContext, string storedProcedure, List<SqlParameter> parameters = null) =>
            new ExecuteDataSetWrapper(dbContext, storedProcedure, parameters);

        public class ExecuteDataSetWrapper
        {
            private readonly DbContext db;
            private readonly string sp;
            private readonly List<SqlParameter> spParameters;

            public ExecuteDataSetWrapper(DbContext dbContext, string storedProcedure, List<SqlParameter> parameters = null)
            {
                this.db = dbContext;
                this.sp = storedProcedure;
                this.spParameters = parameters;
            }

            public DataSet Execute(int commandTimeout = 0)
            {
                var results = new DataSet();
                using (var connection = new SqlConnection(this.db.Database.Connection.ConnectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = this.sp;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = commandTimeout;
                    if (this.spParameters.IsNotNullOrEmpty())
                    {
                        command.Parameters.AddRange(this.spParameters.ToArray());
                    }

                    var adapter = new SqlDataAdapter(command);
                    adapter.Fill(results);
                }

                return results;
            }

            public async Task<DataSet> ExecuteAsync(int commandTimeout = 0)
            {
                var results = new DataSet();
                return await Task.Run(() =>
                {
                    using (var connection = new SqlConnection(this.db.Database.Connection.ConnectionString))
                    {
                        connection.Open();
                        var command = connection.CreateCommand();
                        command.CommandText = this.sp;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = commandTimeout;
                        if (this.spParameters.IsNotNullOrEmpty())
                        {
                            command.Parameters.AddRange(this.spParameters.ToArray());
                        }

                        var adapter = new SqlDataAdapter(command);
                        adapter.Fill(results);
                    }

                    return results;
                })
                .ConfigureAwait(false);
            }
        }
    }
}