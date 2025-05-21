using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

[Table("Vehicles")]
public class Vehicle
{
    public int Id { get; set; }
    [MaxLength(50)]
    public string? UserId { get; set; }
    public int ModelId { get; set; }
    public CarModel Model { get; set; }
    
    public CarType CarType { get; set; }
    public int CarTypeId { get; set; }
    
    public TechState TechState { get; set; }
    public int TechStateId { get; set; }
    public City City { get; set; }
    public int CityId { get; set; }
    
    public int YearOfRelease { get; set; }
    
    [Required]
    [MaxLength(17)]
    public string VINNumber { get; set; }
    public int CarMileage { get; set; }
    
    [MaxLength(2000)]
    public string Description { get; set; }
    public decimal Price { get; set; }
    public bool IsAuction { get; set; }
    public bool IsPaymentInParts { get; set; }
    public bool IsTaxable { get; set; }
    public DateTime LastUpdated { get; set; }
    
    [Required]
    [MaxLength(20)]
    public string PhoneNumber { get; set; }
    public AdvertisementStatus? AdvertisementStatus { get; set; }
    public int? AdvertisementStatusId { get; set; }
}