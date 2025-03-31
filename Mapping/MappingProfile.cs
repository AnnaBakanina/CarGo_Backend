using AutoMapper;
using Backend.Controllers.Resources;
using Backend.Models;

namespace Backend.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CarBrands, CarBrandResource>();
        CreateMap<CarModel, CarModelResource>();
        CreateMap<CarType, CarTypeResource>();
        CreateMap<TechState, TechStateResource>();
        CreateMap<City, CityResource>();
        CreateMap<Region, RegionResource>();
    }
}