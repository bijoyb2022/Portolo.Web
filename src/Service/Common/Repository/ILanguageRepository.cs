using System.Linq;
using Portolo.Common.Data;
using Portolo.Framework.Data;
using Portolo.Services.Common.Data;

namespace Portolo.Common.Repository
{
    public interface ILanguageRepository : IGenericRepository<Language, CommonContext>
    {
        IQueryable<Language> GetLanguages(string displayLanguage);
    }
}