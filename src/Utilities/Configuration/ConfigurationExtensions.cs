using Microsoft.Extensions.Configuration;

namespace Portolo.Utility.Configuration
{
    public static class ConfigurationExtensions
    {
        public static T GetTypedSection<T>(this IConfiguration me)
            where T : ConfigurationSection, new()
        {
            return me.GetSection(new T().SectionName).Get<T>();
        }
    }
}