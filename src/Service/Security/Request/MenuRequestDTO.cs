namespace Portolo.Security.Request
{
    public class MenuRequestDTO
    {
        public int userId { get; set; }
        public int? companyId { get; set; }
        public int navigationId { get; set; }
        public int applicationId { get; set; }
    }
}
