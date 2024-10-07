using Api006.Core.Entities;
using Api006.Service.Dtos.Product;
using AutoMapper;


namespace Api006.Service.Mappings
{
    public class ProductMap : Profile
    {
        public ProductMap()
        {
            CreateMap<ProductPostDto, Product>().ReverseMap();
            CreateMap<ProductPutDto, Product>().ReverseMap();
            CreateMap<Product, ProductGetDto>().ReverseMap();
        }
    }
}
