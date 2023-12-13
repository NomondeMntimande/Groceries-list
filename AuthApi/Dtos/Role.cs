using System.ComponentModel.DataAnnotations;

namespace AuthApi.Dtos
{
    public class Role
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }
    }
}
