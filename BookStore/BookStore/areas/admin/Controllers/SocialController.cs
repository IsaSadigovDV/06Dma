using BookStore.Context;
using BookStore.Extensions;
using BookStore.Helpers;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace BookStore.Areas.admin.Controllers
{
    [Area("admin")]
    public class SocialController : Controller
    {
        private readonly BookDb _context;
        private readonly IWebHostEnvironment _env;

        public SocialController(BookDb context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var socials = await _context.Socials.Where(s=>!s.IsDeleted).ToListAsync();
            return View(socials);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Authors = await _context.Authors.Where(x=>!x.IsDeleted).ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Social social)
        {
            ViewBag.Authors = await _context.Authors.Where(x => !x.IsDeleted).ToListAsync();

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!FileHelper.HasValidSize(social.File, 2))
            {
                ModelState.AddModelError(nameof(File), "File size must be max 2 mb ");
                return View(social);
            };
            if (!FileHelper.IsImage(social.File))
            {
                ModelState.AddModelError(nameof(File), "File must be an image ");
                return View(social);
            };

            social.Icon = await social.File.SaveFileAsync(_env.WebRootPath, "assets/img/social");
            social.CreatedAt = DateTime.UtcNow.AddHours(4);

            await _context.Socials.AddAsync(social);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            ViewBag.Authors = await _context.Authors.Where(x => !x.IsDeleted).ToListAsync();

            var social = await _context.Socials.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            if (social == null) 
            {
                return NotFound();
            }

            return View(social);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, Social social)
        {

            ViewBag.Authors = await _context.Authors.Where(x => !x.IsDeleted).ToListAsync();

            var updatedSocial = await _context.Socials.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            if (updatedSocial == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid) 
            {
                return View(social);
            }

            if(social.File is not null)
            {
                if (!FileHelper.HasValidSize(social.File, 2))
                {
                    ModelState.AddModelError(nameof(File), "File size must be max 2 mb ");
                    return View(social);
                };
                if (!FileHelper.IsImage(social.File))
                {
                    ModelState.AddModelError(nameof(File), "File must be an image ");
                    return View(social);
                };
                FileHelper.DeleteFile(_env.WebRootPath, "assets/img/social", updatedSocial.Icon);

                updatedSocial.Icon = await social.File.SaveFileAsync(_env.WebRootPath, "assets/img/social");
            }

            updatedSocial.UpdatedAt = DateTime.UtcNow.AddHours(4);
            updatedSocial.AuthorId = social.AuthorId;
            updatedSocial.Link = social.Link;
            updatedSocial.Name = social.Name;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var social = await _context.Socials.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            if (social == null)
            {
                return NotFound();
            }
            social.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
