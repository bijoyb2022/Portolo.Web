using Portolo.Utility.Configuration;

namespace Portolo.Utility.FileSystem
{
    public class FileSystemConfig : ConfigurationSection
    {
        public override string SectionName => "FileSystem";
        public string RootFolderPrefix { get; set; }
        public string ConnectionString { get; set; }
    }
}
