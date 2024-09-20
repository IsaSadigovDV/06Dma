using BookStore.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class Author:BaseModel
    {
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public string About {  get; set; }
        public string Country {  get; set; }
        public string Genre {  get; set; }
        public DateTime BirtDate { get; set; }
        public string? Image {  get; set; }

        //notmapped
        [NotMapped]
        public IFormFile? File { get; set; }

        //relations
        [ForeignKey(nameof(Language))]
        public int LanguageId {  get; set; }
        public Language? Language { get; set; }
        public List<Book> Books { get; set; }  = new List<Book>();
        public List<Social> Socials { get; set; } = new List<Social>();
    }
}
