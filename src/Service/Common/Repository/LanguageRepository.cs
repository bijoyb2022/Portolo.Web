using System.Linq;
using Portolo.Common.Data;
using Portolo.Framework.Data;
using Portolo.Services.Common.Data;

namespace Portolo.Common.Repository
{
    public class LanguageRepository : GenericRepository<Language, CommonContext>, ILanguageRepository
    {
        public LanguageRepository(CommonContext context)
            : base(context)
        {
        }

        public IQueryable<Language> GetLanguages(string displayLanguage)
        {
            return this.DbSet.Where(l => l.DisplayLanguage == displayLanguage);
        }
    }
}