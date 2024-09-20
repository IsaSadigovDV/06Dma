using BookStore.Models.Base;

namespace BookStore.Models
{
    public class BookLanguage:BaseModel
    {
        public int LanguageId {  get; set; }
        public Language Language { get; set; }
        public int BookId {  get; set; }
        public Book Book { get; set; }
    }
}
