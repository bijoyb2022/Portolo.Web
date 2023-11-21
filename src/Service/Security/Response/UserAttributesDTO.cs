namespace Portolo.Security.Response
{
    public class UserAttributesDTO
    {
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public string AttributeCode { get; set; }
        public string AttributeName { get; set; }
        public string Value { get; set; }
    }
}
