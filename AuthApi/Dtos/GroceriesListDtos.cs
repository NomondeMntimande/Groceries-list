using System.ComponentModel.DataAnnotations;

namespace AuthApi.Dtos
{
    public class GroceriesListDtos
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string listName { get; set; }
        [Required]
        [MaxLength(200)]
        public string listOwner { get; set; }
        [Required]
        [MaxLength(200)]
        public string categoryListId { get; set; }
        [Required]
        [MaxLength(200)]
        public string listItemId { get; set; }
    }
}
