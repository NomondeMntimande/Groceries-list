using System.ComponentModel.DataAnnotations;

namespace AuthApi.Dtos.Enteties
{
    public class GroceriesList
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
