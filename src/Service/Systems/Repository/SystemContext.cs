using System.Data.Entity;
using Portolo.Framework.Data;

namespace Portolo.Systems.Data
{
    public class SystemContext : AppDbContext
    {
        public SystemContext(string dbConnection)
           : base(dbConnection)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
