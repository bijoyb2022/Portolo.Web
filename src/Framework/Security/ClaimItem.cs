namespace Portolo.Framework.Security
{
    public class ClaimItem
    {
        public ClaimItem()
        {
        }

        public ClaimItem(string resource, string claimType)
        {
            this.Resource = resource;
            this.ClaimType = claimType;
        }

        public string Resource { get; set; }
        public string ClaimType { get; set; }
    }
}