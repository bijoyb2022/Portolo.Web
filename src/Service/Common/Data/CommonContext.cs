using System.Data.Entity;
using Portolo.Common.Data;
using Portolo.Framework.Data;

namespace Portolo.Services.Common.Data
{
    public class CommonContext : AppDbContext
    {
        public CommonContext()
            : base("name=CommonContext")
        {
        }

        public CommonContext(string dbConnection, bool winAuth)
            : base(dbConnection, winAuth)
        {
        }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<LanguageResource> LanguageResource { get; set; }
        public virtual DbSet<LanguageTranslator> LanguageTranslators { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}