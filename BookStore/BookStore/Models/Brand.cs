using BookStore.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class Brand:BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Image {  get; set; }
        [NotMapped]
        public IFormFile? file { get; set; }
    }
}
