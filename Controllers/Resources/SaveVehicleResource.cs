namespace Backend.Controllers.Resources;

public class SaveVehicleResource
{
    public int Id { get; set; }
    
    public int? UserId { get; set; }
    
    public int ModelId { get; set; }
    
    public int CarTypeId { get; set; }
    
    public int TechStateId { get; set; }
    public int? AdvertisementStatusId { get; set; }
    
    public int YearOfRelease { get; set; }
    public decimal Price { get; set; }
    
    public string VINNumber { get; set; }
    public int CarMileage { get; set; }
    
    public string Description { get; set; }
    public bool IsAuction { get; set; }
    public bool IsPaymentInParts { get; set; }
    public bool IsTaxable { get; set; }
    public string PhoneNumber { get; set; }
}