using Microsoft.EntityFrameworkCore;
using MvcFirstCrud.Models;

namespace MvcFirstCrud.Data
{
    public class AppDbContext:DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }

    }
}
