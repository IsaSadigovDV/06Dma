using Api006.App.Dtos.Category;
using Api006.App.Dtos;
using Api006.App.Entities;
using Api006.App.Repositories.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Api006.App.Responses;

namespace Api006.App.Services.Abstractions
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
