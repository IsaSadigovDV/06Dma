using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Context
{
    public class BookDb:DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public BookDb(DbContextOptions<BookDb> options):base(options)
        {
            
        }
    }
}
