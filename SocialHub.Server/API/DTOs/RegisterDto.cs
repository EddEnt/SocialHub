using System.ComponentModel.DataAnnotations;

namespace SocialHub.Server.API.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; }
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\W).{10,}$", 
            ErrorMessage = "Password must be at least 10 characters in length, with lowercase, uppercase, numbers, and non-alphanumeric characters included.")]
        public string Password { get; set; }
    }
}
