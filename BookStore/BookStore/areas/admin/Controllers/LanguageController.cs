using BookStore.Context;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Model;

namespace BookStore.Areas.admin.Controllers
{
    [Area("admin")]
    public class LanguageController : Controller
    {
        private readonly BookDb _context;

        public LanguageController(BookDb context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var languages = await _context.Languages
                .Where(l=>!l.IsDeleted)
                .ToListAsync();
            return View(languages);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Language language)
        {
            if (!ModelState.IsValid) 
            {
                return View(language);
            }
            language.CreatedAt = DateTime.UtcNow.AddHours(4);

            await _context.Languages.AddAsync(language);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            Language? language = await _context.Languages.FirstOrDefaultAsync(l=>l.Id == id && !l.IsDeleted);

            if (language == null) 
            {
                return NotFound();
            }

            return View(language);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id,Language language)
        {
            if (!ModelState.IsValid) 
            {
                return View(language);
            }
            Language? updatedLanguage = await _context.Languages.FirstOrDefaultAsync(l => l.Id == id && !l.IsDeleted);

            if (updatedLanguage == null)
            {
                return NotFound();
            }

            updatedLanguage.Name = language.Name;
            updatedLanguage.UpdatedAt = DateTime.UtcNow.AddHours(4);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            Language? language = await _context.Languages.FirstOrDefaultAsync(l => l.Id == id && !l.IsDeleted);

            if (language == null)
            {
                return NotFound();
            }

            language.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
