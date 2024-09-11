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
            
            return View();
        }
    }
}
