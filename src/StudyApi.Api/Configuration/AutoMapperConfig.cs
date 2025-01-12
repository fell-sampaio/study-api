using AutoMapper;
using StudyApi.Api.DTOs;
using StudyApi.Business.Models;

namespace StudyApi.Api.Configuration;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<SupplierDto, Supplier>().ReverseMap();
        CreateMap<AdressDto, Adress>().ReverseMap();
        CreateMap<ProductDto, Product>();
        CreateMap<ProductImageDto, Product>().ReverseMap();

        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier.Name));
    }
}
