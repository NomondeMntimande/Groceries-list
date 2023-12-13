using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AuthApi.Dtos.Enteties
{
    public class ApplicationRole : IdentityRole<int>
    {
        [Key]
        public override int Id { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }
    }
}
