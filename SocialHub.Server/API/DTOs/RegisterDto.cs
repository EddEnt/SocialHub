namespace SocialHub.Server.API.DTOs
{
    public class RegisterDto
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public string Username { get; set; }
    }
}
