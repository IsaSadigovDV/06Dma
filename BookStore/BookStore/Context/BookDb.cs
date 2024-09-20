using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Context
{
    public class BookDb:DbContext
    {
       
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Social> Socials { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<BookLanguage> BookLanguages { get; set; }
        public DbSet<BookCategories> BookCategories { get; set; }

        public BookDb(DbContextOptions<BookDb> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookLanguage>()
                .HasOne<Book>(c => c.Book)
                .WithMany(b => b.BookLanguages)
                .HasForeignKey(x => x.BookId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<BookLanguage>()
                .HasOne<Language>(c => c.Language)
                .WithMany(b => b.BookLanguages)
                .HasForeignKey(x => x.LanguageId)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }
    }
}
