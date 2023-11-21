namespace Portolo.Framework.Web.Localization
{
    public class ResourceEntry
    {
        public ResourceEntry()
        {
            this.Type = "string";
        }

        public int ResourcesID { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }

        public string Type { get; set; }
    }
}