using Api006.App.Dtos;
using Api006.App.Dtos.Category;
using Api006.App.Entities;
using AutoMapper;

namespace Api006.App.Mappings
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
