using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using Portolo.Framework.Utils;

namespace Portolo.Framework.Data
{
    public static class MultipleResultSets
    {
        public static MultipleResultSetWrapper MultipleResults(this DbContext dbContext,
                                                               string storedProcedure,
                                                               List<SqlParameter> parameters = null) =>
            new MultipleResultSetWrapper(dbContext, storedProcedure, parameters);

        public class MultipleResultSetWrapper
        {
            public List<Func<IObjectContextAdapter, DbDataReader, IEnumerable>> ResultSets;
            private readonly DbContext db;
            private readonly string sp;
            private readonly List<SqlParameter> spParameters;

            public MultipleResultSetWrapper(DbContext dbContext, string storedProcedure, List<SqlParameter> parameters = null)
            {
                this.db = dbContext;
                this.sp = storedProcedure;
                this.spParameters = parameters;
                this.ResultSets = new List<Func<IObjectContextAdapter, DbDataReader, IEnumerable>>();
            }

            public MultipleResultSetWrapper AddToResult<TResult>()
            {
                this.ResultSets.Add((adapter, reader) => adapter.ObjectContext.Translate<TResult>(reader).ToList());
                return this;
            }

            public List<IEnumerable> Execute()
            {
                var results = new List<IEnumerable>();
                using (var connection = new SqlConnection(this.db.Database.Connection.ConnectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = this.sp;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 0;
                    if (this.spParameters.IsNotNullOrEmpty())
                    {
                        command.Parameters.AddRange(this.spParameters.ToArray());
                    }

                    using (var reader = command.ExecuteReader())
                    {
                        var adapter = (IObjectContextAdapter)this.db;
                        foreach (var resultSet in this.ResultSets)
                        {
                            results.Add(resultSet(adapter, reader));
                            reader.NextResult();
                        }
                    }

                    return results;
                }
            }
        }
    }
}