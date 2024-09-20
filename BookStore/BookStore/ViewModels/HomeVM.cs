using BookStore.Models;

namespace BookStore.ViewModels
{
    public class HomeVM
    {
        public List<Blog>? Blogs { get; set; }
        public List<Brand>? Brands { get; set; }
        public List<Book>? Books { get; set; }
        public Author Author { get; set; }
    }
}
