namespace Backend.Controllers.Resources;

public class VehicleResource
{
    public int Id { get; set; }
    public UserResource? User { get; set; }
    public KeyValuePairResource Brand { get; set; }
    public CarModelResource Model { get; set; }
    public CarTypeResource CarType { get; set; }
    public TechStateResource TechState { get; set; }
    
    public int YearOfRelease { get; set; }
    public string VINNumber { get; set; }
    public int CarMileage { get; set; }
    public string Description { get; set; }
    public bool IsAuction { get; set; }
    public bool IsPaymentInParts { get; set; }
    public bool IsTaxable { get; set; }
    public DateTime LastUpdated { get; set; }
    public string PhoneNumber { get; set; }
}