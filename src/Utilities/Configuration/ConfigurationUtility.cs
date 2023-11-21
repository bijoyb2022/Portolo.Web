using System;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace Portolo.Utility.Configuration
{
    public class ConfigurationUtility
    {
        private const string EnvironmentKey = "ASPNETCORE_ENVIRONMENT";
        private static readonly IMemoryCache ConfigurationCache;
        private readonly IConfigurationRoot configRoot;

        static ConfigurationUtility()
        {
            ConfigurationCache = new MemoryCache(new MemoryCacheOptions());
            var environment = GetVariable(EnvironmentKey, false);
            Current = Create(BinDirectory.Get(), environment);
        }

        protected ConfigurationUtility(IConfigurationRoot root)
        {
            this.configRoot = root;
        }

        public static ConfigurationUtility Current { get; }

        public IConfiguration Configuration => this.configRoot;

        public static ConfigurationUtility Create(string baseDirectory, string environment)
        {
            var cacheKey = $"{baseDirectory}|{environment}";

            return ConfigurationCache.GetOrCreate(
                cacheKey,
                entry =>
                {
                    entry.SetOptions(new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15)
                    });

                    var builder = new ConfigurationBuilder()
                        .SetBasePath(baseDirectory)
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true) // defaults
                        .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true) // defaults for environment
                        .AddJsonFile("appsettings.overrides.json", optional: true, reloadOnChange: true) // optional overrides when configuration may be shared
                        .AddJsonFile($"appsettings.{environment}.overrides.json", optional: true, reloadOnChange: true) // optional environment overrides when configuration may be shared
                        .AddJsonFile($"appsettings.overrides.personal.json", optional: true, reloadOnChange: true) // personal developer overrides (.gitignore keeps these local only)
                        .AddEnvironmentVariables(); // highest level override is environment variables

                    var root = builder
                        .Build();

                    return new ConfigurationUtility(root);
                });
        }

        public T GetSection<T>()
            where T : ConfigurationSection, new()
        {
            return this.GetSection<T>(new T().SectionName);
        }

        public string GetSection(string path)
        {
            return this.GetSection<string>(path);
        }

        public T GetSection<T>(string key)
        {
            if (!this.TryGetSection<T>(key, out var result))
            {
                throw new ConfigurationSectionNotFoundException(key, typeof(T));
            }

            return result;
        }

        public bool TryGetSection<T>(out T section)
            where T : ConfigurationSection, new()
        {
            return this.TryGetSection(new T().SectionName, out section);
        }

        public bool TryGetSection<T>(string key, out T section)
        {
            try
            {
                section = this.configRoot.GetSection(key).Get<T>();
                return !section.Equals(default(T));
            }
            catch (Exception)
            {
                section = default;
                return false;
            }
        }

        public void Reload()
        {
            this.configRoot.Reload();
        }

        private static string GetVariable(string variableName, bool throwIfNotDefined)
        {
            var variable = System.Environment.GetEnvironmentVariable(variableName, EnvironmentVariableTarget.Process) ??
                           System.Environment.GetEnvironmentVariable(variableName, EnvironmentVariableTarget.User) ??
                           System.Environment.GetEnvironmentVariable(variableName, EnvironmentVariableTarget.Machine);

            if (string.IsNullOrEmpty(variable) && throwIfNotDefined)
            {
                throw new ArgumentNullException(variableName, $"Environment variable not found or set -- use `SETX {variableName} \"VALUE\"`");
            }

            return variable;
        }
    }
}
