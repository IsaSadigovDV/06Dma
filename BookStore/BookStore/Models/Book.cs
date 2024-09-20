using BookStore.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class Book:BaseModel
    {
        public string Name {  get; set; }
        public double Price { get; set; }
        public int Quantity {  get; set; }
        public string Description { get; set; }
        public string Publisher {  get; set; }
        public int PaperCount {  get; set; }
        public string Dimensions {  get; set; }
        public string? Image {  get; set; }

        //notmapped
        [NotMapped]
        public IFormFile? File { get; set; }

        //relations
        [ForeignKey(nameof(Author))]
        public int AuthorId {  get; set; }
        public Author? Author { get; set; }
        public List<BookCategories>? BookCategories { get; set; }
        public List<BookLanguage>? BookLanguages { get; set; }

        [NotMapped]
        public List<int> CategoryIds { get; set; }
        [NotMapped]
        public List<int> LanguageIds { get; set; }


    }
}
