using AutoMapper;
using StudyApi.Api.DTOs;
using StudyApi.Business.Models;

namespace StudyApi.Api.Mapping;

public class MapProfile: Profile
{
    public MapProfile()
    {
        CreateMap<SupplierDto, Supplier>().ReverseMap();
        CreateMap<AdressDto, Adress>().ReverseMap();
        CreateMap<ProductDto, Product>();
    }
}
