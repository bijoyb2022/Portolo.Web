using System.Data.Entity;
using Portolo.Framework.Data;

namespace Portolo.Pages.Data
{
    public class PagesContext : AppDbContext
    {
        public PagesContext(string dbConnection)
           : base(dbConnection)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
