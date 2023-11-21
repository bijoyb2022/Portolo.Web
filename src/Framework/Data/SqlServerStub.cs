using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;

namespace Portolo.Framework.Data
{
    internal static class SqlServerStub
    {
        private static SqlProviderServices forceEfSqlServerDll = SqlProviderServices.Instance;
    }
}
