using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcFirstCrud.Data;
using MvcFirstCrud.Models;

namespace MvcFirstCrud.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories
                .Where(x=>!x.IsDeleted)
                .AsNoTracking()
                .ToListAsync();
            return View(categories);
        }
        public IActionResult Create()
        {
            Category category = new Category();
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid) 
            {
                return View(category);
            }
            category.CreatedAt = DateTime.UtcNow.AddHours(4);
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id) 
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category is null) 
            {
                return NotFound();
            }
            category.IsDeleted = true;
            //_context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Update(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category is null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            var updatedCategory = await _context.Categories.FirstOrDefaultAsync(c=>c.Id == id);
            if(updatedCategory is null)
            {
                return View(category);
            }
            updatedCategory.Name = category.Name;
            updatedCategory.Description = category.Description;
            updatedCategory.UpdatedAt = DateTime.UtcNow.AddHours(4);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
