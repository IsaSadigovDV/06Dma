using BookStore.Context;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly BookDb _context;

        public HomeController(BookDb context)
        {
            _context = context;
        }

        public async Task< IActionResult> Index()
        {
            HomeVM homeVM = new HomeVM()
            {
              Blogs = await _context.Blogs
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.CreatedAt)
                .Take(3)
                .ToListAsync(),
              Brands = await _context.Brands
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync(),
              Books = await _context.Books
              .Where(b=>!b.IsDeleted)
              .Include(b=>b.BookCategories)
              .ThenInclude(b=>b.Category)
              .OrderByDescending(b=>b.CreatedAt)
              .ToListAsync(),
              Author = await _context.Authors.FirstOrDefaultAsync(x=>x.Id ==2)
            };

            return View(homeVM);
        }

    }
}
