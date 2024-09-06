using BookStore.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class Blog:BaseModel
    {
        public string? Image {  get; set; }
        public string Explanation {  get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        //relations
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        [NotMapped]
        public IFormFile? File { get; set; }
    }
}
