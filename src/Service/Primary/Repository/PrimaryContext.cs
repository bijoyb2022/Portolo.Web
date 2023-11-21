using System.Data.Entity;
using Portolo.Framework.Data;

namespace Portolo.Primary.Data
{
    public class PrimaryContext : AppDbContext
    {
        public PrimaryContext(string dbConnection)
           : base(dbConnection)
        {
        }
        //public virtual DbSet<Countries> Countries { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
