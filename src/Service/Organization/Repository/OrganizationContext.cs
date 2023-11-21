using System.Data.Entity;
using Portolo.Framework.Data;

namespace Portolo.Organization.Data
{
    public class OrganizationContext : AppDbContext
    {
        public OrganizationContext(string dbConnection)
           : base(dbConnection)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
