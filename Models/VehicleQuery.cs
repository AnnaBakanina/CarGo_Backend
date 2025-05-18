using Backend.Extensions;

namespace Backend.Models;

public class VehicleQuery : IQueryObject
{
    public string? UserId { get; set; }
    public int? BrandId { get; set; }
    public int? ModelId { get; set; }
    public int? CarTypeId { get; set; }
    public int? TechStateId { get; set; }
    public int? RegionId { get; set; }
    public int? CityId { get; set; }
    
    public string SortBy { get; set; }
    public bool IsSortAscending { get; set; }
    
    public decimal? PriceFrom { get; set; }
    public decimal? PriceTo { get; set; }
    
    public int? CarMileageFrom { get; set; }
    public int? CarMileageTo { get; set; }
    
    public int Page { get; set; }
    public int PageSize { get; set; }
}