namespace Portolo.Security.Request
{
    public class AuthenticateUserDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int ServiceModuleId { get; set; }
    }
}
