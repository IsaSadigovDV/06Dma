using BookStore.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers
{
    public class BlogController : Controller
    {
        private readonly BookDb _context;
        public BlogController(BookDb context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var blogs = await _context.Blogs.Where(x => !x.IsDeleted).ToListAsync();

            return View(blogs);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var blog = await _context.Blogs
                .Include(b=>b.Category)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (blog == null) 
            {
                return NotFound();
            }
            return View(blog);
        }
    }
}
