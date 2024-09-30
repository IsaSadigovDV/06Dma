using Api006.App.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api006.App.Context
{
    public class AppDbContext:DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
    }
}
