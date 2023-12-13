using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AuthApi.Dtos.Enteties
{
    public class User : IdentityUser<int>
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public override string Email { get; set; }

        [Required]
        public override string UserName { get; set; }
    }
}
