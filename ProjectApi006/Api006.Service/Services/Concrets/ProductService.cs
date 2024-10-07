using Api006.Core.Entities;
using Api006.Core.Repositories.Abstractions;
using Api006.Data.Context;
using Api006.Service.Dtos.Product;
using Api006.Service.Extensions;
using Api006.Service.Responses;
using Api006.Service.Services.Abstractions;
using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api006.Service.Services.Concrets
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepo;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProductService(IMapper mapper, IProductRepository repository, IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _productRepo = repository;
            _env = env;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResponse> Create(ProductPostDto dto)
        {
            Product product = _mapper.Map<Product>(dto);
            product.Image = await dto.File.SaveFileAsync(_env.WebRootPath, "assets/img/product");
            //https://localhost:7085/assets/img/product/6196b475-28ce-42d4-81c8-6f6b51eb88e1sefiller.jpeg
            var reqest = _httpContextAccessor.HttpContext.Request;
            product.ImageUrl = reqest.Scheme+ "://" + reqest.Host + $"/assets/img/product/{product.Image}";

            await _productRepo.AddAsync(product);
            await _productRepo.SaveAsync();
            return new ApiResponse { StatusCode = 201 };
        }

        public async Task<ApiResponse> GetAll()
        {
            var products = _productRepo.GetAllWhere(x => !x.IsDeleted);
            List<ProductGetDto> dtos = await products.Select(x => new ProductGetDto { Id =x.Id,Price = x.Price, Name = x.Name, Image = x.Image, Description = x.Description, ImageUrl = x.ImageUrl, Category = x.Category }).ToListAsync();
            return new ApiResponse { StatusCode = 200, Data = dtos };
        }

        public async Task<ApiResponse> GetById(Guid id)
        {
            var product = await _productRepo.GetByIdAsync(x => !x.IsDeleted && x.Id == id);

            if (product == null)
                return new ApiResponse { StatusCode = 404, Message = "Item is not found" };
            ProductGetDto dto = _mapper.Map<ProductGetDto>(product);
            return new ApiResponse {StatusCode = 200, Data =dto };
        }     

        public async Task<ApiResponse> Remove(Guid id)
        {
            var product = await _productRepo.GetByIdAsync(x => !x.IsDeleted && x.Id == id);

            if (product == null)
                return new ApiResponse { StatusCode = 404, Message = "Item is not found" };
            product.IsDeleted = true;
            await _productRepo.SaveAsync();
            return new ApiResponse { StatusCode = 200 };
        }

        public async Task<ApiResponse> Update(Guid id, ProductPutDto dto)
        {
            var product = await _productRepo.GetByIdAsync(x => !x.IsDeleted && x.Id == id);

            if (product == null)
                return new ApiResponse { StatusCode = 404, Message = "Item is not found" };

            product.Price = dto.Price;
            product.Description = dto.Description;
            product.Name = dto.Name;
            product.CategoryId = dto.CategoryId;
            product.Image = dto.File == null ? product.Image : await dto.File.SaveFileAsync(_env.WebRootPath, "assets/img/product");
            return new ApiResponse { StatusCode = 204 };
        }
    }
}
