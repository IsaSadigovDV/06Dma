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
    public class BookController : Controller
    {
        private readonly BookDb _context;
        private readonly IWebHostEnvironment _env;
        ////ctrl + ,

        public BookController(BookDb context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _context.Books
                .Where(b=>!b.IsDeleted)
                .Include(b=>b.Author).Where(a=>!a.IsDeleted)
                .ToListAsync();
            return View(books);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Languages = await _context.Languages.Where(l => !l.IsDeleted).ToListAsync();
            ViewBag.Categories = await _context.Categories.Where(c => !c.IsDeleted).ToListAsync();
            ViewBag.Authors = await _context.Authors.Where(x => !x.IsDeleted).ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Book book)
        {
            ViewBag.Languages = await _context.Languages.Where(l => !l.IsDeleted).ToListAsync();
            ViewBag.Categories = await _context.Categories.Where(c => !c.IsDeleted).ToListAsync();
            ViewBag.Authors = await _context.Authors.Where(x => !x.IsDeleted).ToListAsync();

            if (!ModelState.IsValid)
            {
                return View(book);
            }
            // sekil meselesi
            if (!FileHelper.HasValidSize(book.File, 2))
            {
                ModelState.AddModelError(nameof(File), "File size must be max 2 mb ");
                return View(book);
            };
            if (!FileHelper.IsImage(book.File))
            {
                ModelState.AddModelError(nameof(File), "File must be an image ");
                return View(book);
            };
            book.Image = await book.File.SaveFileAsync(_env.WebRootPath, "assets/img/book");

            // category ile many to many logici duzeltmek
            foreach (var item in book.CategoryIds)
            {
                BookCategories bookCategories = new BookCategories()
                {
                    Book = book,
                    CategoryId = item,
                    CreatedAt = DateTime.UtcNow.AddHours(4)
                };

                await _context.BookCategories.AddRangeAsync(bookCategories);
            }

            // language ile many to many logici duzeltmek
            foreach (var item in book.LanguageIds)
            {
                BookLanguage bookLanguage = new BookLanguage()
                {
                    Book = book,
                    LanguageId = item,
                    CreatedAt = DateTime.UtcNow.AddHours(4)
                };

                await _context.BookLanguages.AddRangeAsync(bookLanguage);
            }

            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> Update(int id)
        {
            ViewBag.Languages = await _context.Languages.Where(l => !l.IsDeleted).ToListAsync();
            ViewBag.Categories = await _context.Categories.Where(c => !c.IsDeleted).ToListAsync();
            ViewBag.Authors = await _context.Authors.Where(x => !x.IsDeleted).ToListAsync();

            Book? book = await _context.Books
                .Include(b=>b.Author)
                .Include(b=>b.BookLanguages)
                .ThenInclude(l=>l.Language).Where(x=>!x.IsDeleted)
                .Include(c=>c.BookCategories)
                .ThenInclude(c=>c.Category).Where(x=>!x.IsDeleted)
                .FirstOrDefaultAsync(b=>!b.IsDeleted && b.Id==id);

            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, Book book)
        {
            ViewBag.Languages = await _context.Languages.Where(l => !l.IsDeleted).ToListAsync();
            ViewBag.Categories = await _context.Categories.Where(c => !c.IsDeleted).ToListAsync();
            ViewBag.Authors = await _context.Authors.Where(x => !x.IsDeleted).ToListAsync();

            Book? updatedbook = await _context.Books
             .Include(b => b.Author)
             .Include(b => b.BookLanguages)
             .ThenInclude(l => l.Language).Where(x => !x.IsDeleted)
             .Include(c => c.BookCategories)
             .ThenInclude(c => c.Category).Where(x => !x.IsDeleted)
             .FirstOrDefaultAsync(b => !b.IsDeleted && b.Id == id);

            if (!ModelState.IsValid)
            {
                return View(updatedbook);
            }
            if(book.File != null)
            {
                if (!FileHelper.HasValidSize(book.File, 2))
                {
                    ModelState.AddModelError(nameof(File), "File size must be max 2 mb ");
                    return View(book);
                };
                if (!FileHelper.IsImage(book.File))
                {
                    ModelState.AddModelError(nameof(File), "File must be an image ");
                    return View(book);
                };

                FileHelper.DeleteFile(_env.WebRootPath, "assets/img/blog", updatedbook.Image);

                updatedbook.Image = await book.File.SaveFileAsync(_env.WebRootPath, "assets/img/book");
            }

            //List<BookCategories> bookCategories = await _context.BookCategories.AnyAsync(x=>x.BookId==id.con);

            foreach (var item in book.CategoryIds)
            {
                BookCategories bookCategories = new BookCategories()
                {
                    BookId = book.Id,
                    CategoryId = item,
                    UpdatedAt = DateTime.UtcNow.AddHours(4),
                };
                await _context.BookCategories.AddRangeAsync(bookCategories);
            }

            foreach(var item in book.LanguageIds)
            {
                BookLanguage bookLanguage = new BookLanguage()
                {
                    BookId = book.Id,
                    LanguageId = item,
                    CreatedAt = DateTime.UtcNow.AddHours(4)
                };
                await _context.BookLanguages.AddRangeAsync(bookLanguage);
            }

            updatedbook.Name = book.Name;
            updatedbook.Description = book.Description;
            updatedbook.PaperCount = book.PaperCount;
            updatedbook.AuthorId = book.AuthorId;
            updatedbook.Dimensions = book.Dimensions;
            updatedbook.Publisher = book.Publisher;
            updatedbook.Price = book.Price;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            if (book == null) 
            {
                return NotFound();
            }
            book.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
