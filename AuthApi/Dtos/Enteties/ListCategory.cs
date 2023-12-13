using System.ComponentModel.DataAnnotations;

namespace AuthApi.Dtos.Enteties
{
    public class ListCategory
    {
        public int Id { get; set; }

        [MaxLength(255)]
        public string Description { get;set; }
    }
}
