using Api006.Service.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Api006.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var data =  await _categoryService.GetAll();
            if (data.StatusCode != 200) 
            {
                return NotFound();
            }
            return View(data.Data);
        }
    }
}
