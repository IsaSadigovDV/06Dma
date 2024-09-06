using BookStore.Context;
using BookStore.Extensions;
using BookStore.Helpers;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace BookStore.Areas.admin.Controllers
{
    [Area("admin")]
    public class BlogController : Controller
    {
        private readonly BookDb _context;
        private IWebHostEnvironment _env;
        public BlogController(BookDb context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var blogs = await _context.Blogs
                .Where(b => !b.IsDeleted)
                .Include(b => b.Category).Where(c => !c.IsDeleted)
                .ToListAsync();
            return View(blogs);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _context.Categories.Where(c => !c.IsDeleted).ToListAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blog blog)
        {
            ViewBag.Categories = await _context.Categories.Where(c => !c.IsDeleted).ToListAsync();
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!FileHelper.HasValidSize(blog.File, 2))
            {
                ModelState.AddModelError(nameof(File), "File size must be max 2 mb ");
                return View(blog);
            };
            if (!FileHelper.IsImage(blog.File))
            {
                ModelState.AddModelError(nameof(File), "File must be an image ");
                return View(blog);
            };


            blog.Image = await blog.File.SaveFileAsync(_env.WebRootPath, "assets/img/blog");
            await _context.Blogs.AddAsync(blog);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            ViewBag.Categories = await _context.Categories.Where(c => !c.IsDeleted).ToListAsync();
            var blog = await _context.Blogs
                .Include(b => b.Category)
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Blog blog)
        {
            ViewBag.Categories = await _context.Categories.Where(c => !c.IsDeleted).ToListAsync();
            var updatedblog = await _context.Blogs
                .Include(b => b.Category)
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
            if (updatedblog == null)
            {
                return View(updatedblog);
            }
            if (!ModelState.IsValid)
            {
                return View(updatedblog);
            }

            if (blog.File is not null)
            {
                if (!FileHelper.HasValidSize(blog.File, 5))
                {
                    ModelState.AddModelError(nameof(File), "File size must be max 2 mb ");
                    return View();
                };
                if (!FileHelper.IsImage(blog.File))
                {
                    ModelState.AddModelError(nameof(File), "File must be an image ");
                    return View();
                };

                updatedblog.Image = await blog.File.SaveFileAsync(_env.WebRootPath, "assets/img/blog");
            }
            updatedblog.UpdatedAt = DateTime.UtcNow.AddHours(4);
            updatedblog.Title = blog.Title;
            updatedblog.Description = blog.Description;
            updatedblog.Explanation = blog.Explanation;
            updatedblog.CategoryId = blog.CategoryId;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var blog = await _context.Blogs
                .Include(b => b.Category)
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
            if (blog == null)
            {
                return NotFound();
            }

            //_context.Blogs.Remove(blog);
            blog.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
