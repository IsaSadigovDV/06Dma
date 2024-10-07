using Api006.Service.Dtos;
using Api006.Service.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Api006.Service.Responses;

namespace Api006.Service.Services.Abstractions
{
    public interface ICategoryService
    {
        public Task<ApiResponse> GetAll();
        public  Task<ApiResponse> GetById(Guid id);
        public Task<ApiResponse> Create(CategoryPostDto dto);
        public Task<ApiResponse> Update(Guid id, CategoryPutDto dto);
        public  Task<ApiResponse> Remove(Guid id);

    }
}
