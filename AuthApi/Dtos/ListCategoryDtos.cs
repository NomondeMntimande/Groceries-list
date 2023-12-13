using System.ComponentModel.DataAnnotations;

namespace AuthApi.Dtos
{
    public class ListCategoryDtos
    {
        public int Id { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }
    }
}
