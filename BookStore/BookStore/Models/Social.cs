using BookStore.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class Social:BaseModel
    {
        public string Link {  get; set; }
        public string? Icon { get; set; }
        public string Name {  get; set; }

        [NotMapped]
        public IFormFile? File { get; set; }
        //relations
        [ForeignKey(nameof(Author))]
        public int? AuthorId {  get; set; }
        public Author? Author { get; set; }

    }
}
