
using Api006.App.Dtos;
using Api006.App.Dtos.Category;
using Api006.App.Entities;
using Api006.App.Repositories.Abstractions;
using Api006.App.Responses;
using Api006.App.Services.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api006.App.Services.Concrets
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepo;

        public CategoryService(IMapper mapper, ICategoryRepository categoryRepo)
        {
            _mapper = mapper;
            _categoryRepo = categoryRepo;
        }

        public async Task<ApiResponse> Create(CategoryPostDto dto)
        {
            Category category = _mapper.Map<Category>(dto);
            await _categoryRepo.AddAsync(category);
            await _categoryRepo.SaveAsync();
            return new ApiResponse { StatusCode=201};
        }

        public async Task<ApiResponse> GetAll()
        {
            var categories = _categoryRepo.GetAllWhere(x=>!x.IsDeleted);
            List<CategoryGetDto> dtos = await categories.Select(c => new CategoryGetDto { Id = c.Id, Name = c.Name }).ToListAsync();
            return new ApiResponse { StatusCode=200, Data=dtos};
        }

        public async Task<ApiResponse> GetById(Guid id)
        {
            var category = await _categoryRepo.GetByIdAsync(x => x.Id == id && !x.IsDeleted);
            if (category == null)
                return new ApiResponse { StatusCode=404,Data="" ,Message="Item is not found" };
            CategoryGetDto dto = _mapper.Map<CategoryGetDto>(category);
            return new ApiResponse { StatusCode=200,Data=dto};
        }

        public async Task<ApiResponse> Remove(Guid id)
        {
            var category = await _categoryRepo.GetByIdAsync(x => x.Id == id);
            if (category == null)
                return new ApiResponse { StatusCode = 404, Message = "Item is not found" };
            category.IsDeleted = true;
            //_categoryRepo.Delete(category);
            await _categoryRepo.SaveAsync();
            return new ApiResponse { StatusCode = 204};
        }

        public async Task<ApiResponse> Update(Guid id, CategoryPutDto dto)
        {
            var updatedCategory = await _categoryRepo.GetByIdAsync(x => x.Id == id);
            if (updatedCategory == null)
                return new ApiResponse { StatusCode = 404, Message = "Item is not found" };
            //updatedCategory = _mapper.Map<Category>(dto);
            updatedCategory.Name = dto.Name;
            await _categoryRepo.SaveAsync();
            return new ApiResponse { StatusCode = 204};
        }
    }
}
