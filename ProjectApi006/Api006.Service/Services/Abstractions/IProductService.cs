using Api006.Data.Context;
using Api006.Service.Dtos;
using Api006.Service.Dtos;
using Api006.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api006.Service.Dtos.Product;

namespace Api006.Service.Services.Abstractions
{
    public interface IProductService
    {
        public Task<ApiResponse> GetAll();
        public Task<ApiResponse> GetById(Guid id);
        public Task<ApiResponse> Create(ProductPostDto dto);
        public Task<ApiResponse> Update(Guid id, ProductPutDto dto);
        public Task<ApiResponse> Remove(Guid id);
    }
}
