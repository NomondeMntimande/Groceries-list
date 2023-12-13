using System.ComponentModel.DataAnnotations;

namespace AuthApi.Dtos
{
    public class Authentication
    {
        [Required(ErrorMessage = "Username/Email is required")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = null!;
    }
}
