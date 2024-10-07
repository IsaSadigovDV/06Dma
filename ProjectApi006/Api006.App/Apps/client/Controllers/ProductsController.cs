using Api006.Service.Dtos;
using Api006.Service.Dtos.Product;
using Api006.Service.Services.Abstractions;
using Api006.Service.Services.Concrets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api006.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // todo: fronta biz imageUrl vermeliyik ki o shekilden bir basa istifade ede bilsin

        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = await _productService.GetAll();
            return StatusCode(res.StatusCode, res.Data);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var res = await _productService.GetById(id);
            return StatusCode(res.StatusCode, res.Data);
        }
    }
}
