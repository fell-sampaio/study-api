using AutoMapper;
using StudyApi.Api.DTOs;
using StudyApi.Business.Models;

namespace StudyApi.Api.Mapping;

public class MapProfile: Profile
{
    public MapProfile()
    {
        CreateMap<Supplier, SupplierDto>().ReverseMap();
        CreateMap<Adress, AdressDto>().ReverseMap();
        CreateMap<Product, ProductDto>().ReverseMap();
    }
}
