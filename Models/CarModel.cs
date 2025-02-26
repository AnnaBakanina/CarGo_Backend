using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class CarModel
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(255)]
    public string Name { get; set; }
    public CarBrands Brand { get; set; }
    public int BrandId { get; set; }
    
}