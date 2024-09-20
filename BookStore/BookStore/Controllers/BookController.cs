using BookStore.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private  readonly BookDb _context;

        public BookController(BookDb context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var books= await _context.Books
                .Where(x=>!x.IsDeleted)
                .Include(x=>x.BookCategories)
                .ThenInclude(x=>x.Category)
                .OrderByDescending(b=>b.CreatedAt)
                .ToListAsync();

            return View(books);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var book = await _context.Books
                .Include(b=>b.BookCategories)
                .ThenInclude(b=>b.Category)
                .Include(b=>b.BookLanguages)
                .ThenInclude(b=>b.Language)
                .FirstOrDefaultAsync(b=>b.Id==id && !b.IsDeleted);
            if(book==null)
                return NotFound();

            return View(book);
        }
    }
}
