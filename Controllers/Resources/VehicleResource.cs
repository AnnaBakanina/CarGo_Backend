using Backend.Models;

namespace Backend.Controllers.Resources;

public class VehicleResource
{
    public int Id { get; set; }
    public KeyValuePairResource Brand { get; set; }
    public KeyValuePairResource Model { get; set; }
    public CarTypeResource CarType { get; set; }
    public TechStateResource TechState { get; set; }
    public KeyValuePairResource Region { get; set; }
    public KeyValuePairResource City { get; set; }
    public AdvertisementStatus? AdvertisementStatus { get; set; }
    
    public int YearOfRelease { get; set; }
    public decimal Price { get; set; }
    public string VINNumber { get; set; }
    public int CarMileage { get; set; }
    public string Description { get; set; }
    public bool IsAuction { get; set; }
    public bool IsPaymentInParts { get; set; }
    public bool IsTaxable { get; set; }
    public DateTime LastUpdated { get; set; }
    public string PhoneNumber { get; set; }
}