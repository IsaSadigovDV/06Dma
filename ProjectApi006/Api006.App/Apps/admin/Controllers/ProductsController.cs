using Api006.Service.Dtos.Product;
using Api006.Service.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Api006.App.Apps.admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // todo: fronta biz imageUrl vermeliyik ki o shekilden bir basa istifade ede bilsin

        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
       
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductPostDto dto)
        {
            var res = await _productService.Create(dto);
            return Ok(res);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, ProductPutDto dto)
        {
            var res = await _productService.Update(id, dto);
            return StatusCode(res.StatusCode);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            var res = await _productService.Remove(id);
            return StatusCode(res.StatusCode);
        }
    }
}
