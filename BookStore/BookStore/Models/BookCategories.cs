using BookStore.Models.Base;

namespace BookStore.Models
{
    public class BookCategories:BaseModel
    {
        public int BookId {  get; set; }
        public int CategoryId {  get; set; }

        // navigations
        public Category Category { get; set; }
        public Book Book { get; set; }
    }
}
