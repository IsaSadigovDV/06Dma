using BookStore.Context;
using BookStore.Extensions;
using BookStore.Helpers;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace authorStore.Areas.admin.Controllers
{
    [Area("admin")]
    public class AuthorController : Controller
    {
        private readonly BookDb _context;
        private readonly IWebHostEnvironment _env;

        public AuthorController(BookDb context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var author = await _context.Authors
                .Where(a=>!a.IsDeleted)
                .ToListAsync();
            return View(author);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Languages = await _context.Languages.Where(l=>!l.IsDeleted).ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Author author)
        {
            ViewBag.Languages = await _context.Languages.Where(l => !l.IsDeleted).ToListAsync();
            if (!ModelState.IsValid) 
            {
                return View();
            }

            // sekil meselesi
            if (!FileHelper.HasValidSize(author.File, 2))
            {
                ModelState.AddModelError(nameof(File), "File size must be max 2 mb ");
                return View(author);
            };
            if (!FileHelper.IsImage(author.File))
            {
                ModelState.AddModelError(nameof(File), "File must be an image ");
                return View(author);
            };
            author.Image = await author.File.SaveFileAsync(_env.WebRootPath, "assets/img/author");

            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(l => l.Id == id);

            if (author == null) return NotFound();
            author.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
