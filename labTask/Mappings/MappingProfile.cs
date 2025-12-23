using AutoMapper;
using labTask.Models;
using labTask.DTOs;

namespace labTask.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Category, CategoryDto>();
        CreateMap<CreateCategoryDto, Category>();
        CreateMap<UpdateCategoryDto, Category>();
        
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category!.Name));
        CreateMap<CreateProductDto, Product>();
        CreateMap<UpdateProductDto, Product>();
        
        CreateMap<Category, CategoryWithProductsDto>()
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));
    }
}
