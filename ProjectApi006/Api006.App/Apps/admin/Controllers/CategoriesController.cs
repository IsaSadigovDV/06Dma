using Api006.Service.Dtos;
using Api006.Service.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Api006.App.Apps.admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin, SuperAdmin")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CategoryPostDto dto)
        {
            var res = await _categoryService.Create(dto);
            return StatusCode(res.StatusCode);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, CategoryPutDto dto)
        {
            var res = await _categoryService.Update(id, dto);

            return StatusCode(res.StatusCode);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            var res = await _categoryService.Remove(id);
            return StatusCode(res.StatusCode);
        }

    }
}
