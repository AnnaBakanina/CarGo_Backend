using AutoMapper;
using Backend.Controllers.Resources;
using Backend.Models;

namespace Backend.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Domain to API Resource
        CreateMap(typeof(QueryResult<>), typeof(QueryResultResource<>));
        CreateMap<CarBrands, CarBrandResource>();
        CreateMap<CarBrands, KeyValuePairResource>();
        CreateMap<CarModel, CarModelResource>();
        CreateMap<CarType, CarTypeResource>();
        CreateMap<TechState, TechStateResource>();
        CreateMap<City, CityResource>();
        CreateMap<Region, RegionResource>();
        CreateMap<Vehicle, SaveVehicleResource>();
        CreateMap<Vehicle, VehicleResource>()
            .ForMember(vr => vr.Brand, opt => opt.MapFrom(v => v.Model.Brand));
        CreateMap<User, UserResource>();
        
        // API Resource to Domain model
        CreateMap<SaveVehicleResource, Vehicle>()
            .ForMember(v => v.Id, opt => opt.Ignore())
            .ForMember(v => v.UserId, opt => opt.MapFrom(vr => vr.UserId))
            .ForMember(v => v.ModelId, opt => opt.MapFrom(vr => vr.ModelId))
            .ForMember(v => v.CarTypeId, opt => opt.MapFrom(vr => vr.CarTypeId))
            .ForMember(v => v.TechStateId, opt => opt.MapFrom(vr => vr.TechStateId))
            .ForMember(v => v.YearOfRelease, opt => opt.MapFrom(vr => vr.YearOfRelease))
            .ForMember(v => v.VINNumber, opt => opt.MapFrom(vr => vr.VINNumber))
            .ForMember(v => v.CarMileage, opt => opt.MapFrom(vr => vr.CarMileage))
            .ForMember(v => v.Description, opt => opt.MapFrom(vr => vr.Description))
            .ForMember(v => v.IsAuction, opt => opt.MapFrom(vr => vr.IsAuction))
            .ForMember(v => v.IsPaymentInParts, opt => opt.MapFrom(vr => vr.IsPaymentInParts))
            .ForMember(v => v.IsTaxable, opt => opt.MapFrom(vr => vr.IsTaxable))
            .ForMember(v => v.PhoneNumber, opt => opt.MapFrom(vr => vr.PhoneNumber))
            .ForMember(v => v.LastUpdated, opt => opt.MapFrom(_ => DateTime.UtcNow));
        CreateMap<UserResource, User>()
            .ForMember(u => u.Id, opt => opt.Ignore())
            .ForMember(v => v.LastUpdated, opt => opt.MapFrom(_ => DateTime.UtcNow));
        CreateMap<VehicleQueryResource, VehicleQuery>();
    }
}