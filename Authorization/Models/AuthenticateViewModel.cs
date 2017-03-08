using System.ComponentModel.DataAnnotations;

namespace Authorization.Models
{
    public class AuthenticateViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}