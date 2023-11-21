using System.Data.Entity;
using Portolo.Framework.Data;

namespace Portolo.Security.Data
{
    public class SecurityContext : AppDbContext
    {
        public SecurityContext(string dbConnection)
            : base(dbConnection)
        {
        }

    }
}
