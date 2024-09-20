using BookStore.Context;
using BookStore.Extensions;
using BookStore.Helpers;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Areas.admin.Controllers
{
    [Area("admin")]
    public class BrandController : Controller
    {
        private readonly BookDb _context;
        private readonly IWebHostEnvironment _env;

        public BrandController(BookDb context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var brands = await _context.Brands.Where(b => !b.IsDeleted).ToListAsync();
            return View(brands);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Brand brand)
        {
            if (!ModelState.IsValid)
            {
                return View();

            }
            if (!FileHelper.IsImage(brand.file))
            {
                ModelState.AddModelError(nameof(File), "File is not an image");
                return View();
            }
            if (!FileHelper.HasValidSize(brand.file, 5))
            {
                ModelState.AddModelError(nameof(File), "File size is not valid");
                return View();
            }


            brand.Image = await brand.file.SaveFileAsync(_env.WebRootPath, "assets/img/brand");
            brand.CreatedAt = DateTime.UtcNow.AddHours(4);
            await _context.Brands.AddAsync(brand);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var brand = await _context.Brands.FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Brand brand)
        {
            var updatedbrand = await _context.Brands.FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted);
            if (brand == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(updatedbrand);
            }

            if (brand.file != null)
            {
                if (!FileHelper.IsImage(brand.file))
                {
                    ModelState.AddModelError(nameof(File), "File is not an image");
                    return View();
                }
                if (!FileHelper.HasValidSize(brand.file, 5))
                {
                    ModelState.AddModelError(nameof(File), "File size is not valid");
                    return View();
                }
                FileHelper.DeleteFile(_env.WebRootPath, "assets/img/brand", updatedbrand.Image);

                brand.Image = await brand.file.SaveFileAsync(_env.WebRootPath, "assets/img/brand");
            }

            updatedbrand.Description = brand.Description;
            updatedbrand.Name = brand.Name;
            updatedbrand.UpdatedAt = DateTime.UtcNow.AddHours(4);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var brand = await _context.Brands.FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted);
            if (brand == null)
            {
                return NotFound();
            }
            brand.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
