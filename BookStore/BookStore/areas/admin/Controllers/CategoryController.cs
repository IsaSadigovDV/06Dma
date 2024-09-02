using BookStore.Context;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Areas.admin.Controllers
{
    [Area("admin")]
    public class CategoryController : Controller
    {
        private readonly BookDb _context;

        public CategoryController(BookDb context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var categories =  await _context.Categories.Where(c=>!c.IsDeleted).ToListAsync();
            return View(categories);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid) 
            {
                return View(category);
            }

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
