using Api006.Service.Dtos;
using Api006.Service.Dtos;
using Api006.Core.Entities;
using AutoMapper;

namespace Api006.Service.Mappings
{
    public class CategoryMap:Profile
    {
        public CategoryMap()
        {
            CreateMap<CategoryPostDto, Category>().ReverseMap();
            CreateMap<CategoryPutDto, Category>().ReverseMap();
            CreateMap<Category, CategoryGetDto>().ReverseMap();
        }
    }
}
