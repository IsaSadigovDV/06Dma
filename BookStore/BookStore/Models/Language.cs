using BookStore.Models.Base;

namespace BookStore.Models
{
    public class Language:BaseModel
    {
        public string Name {  get; set; }
        public List<Author>? Authors { get; set; }

        //relations
        public List<BookLanguage>? BookLanguages { get; set; }
    }
}
