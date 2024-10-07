using Api006.Service.Dtos;
using Api006.Service.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;


namespace Api006.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = await _categoryService.GetAll();
            return StatusCode(res.StatusCode, res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var res = await _categoryService.GetById(id);
            return StatusCode(res.StatusCode, res);
        }
    }
}
