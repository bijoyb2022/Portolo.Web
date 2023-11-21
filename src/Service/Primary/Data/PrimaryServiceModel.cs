using System.Data.Entity;

namespace Portolo.Primary.Data
{
    public class PrimaryServiceModel : DbContext
    {
        public PrimaryServiceModel()
            : base("name=PrimaryServiceModel")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {            
        }
    }
}