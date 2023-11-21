namespace Portolo.Framework.Web.Localization
{
    public interface IResourceProvider
    {
        object GetResource(string name, string culture);
    }
}