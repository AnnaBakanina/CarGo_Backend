using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class CarBrands
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(255)]
    public string Name { get; set; }
    public ICollection<CarModel> CarModel { get; set; }

    public CarBrands()
    {
        CarModel = new Collection<CarModel>();
    }
}