namespace Portolo.Security.Request
{
    public class UserSearchDTO
    {
        public string SearchText { get; set; }
        public string UserType { get; set; }
        public int UserId { get; set; }
        public int OwnerId { get; set; }
        public int CompanyId { get; set; }
    }
}
