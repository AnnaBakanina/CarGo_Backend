using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class CarBrands
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(255)]
    public string Name { get; set; }
    public ICollection<CarModel> Models { get; set; }

    public CarBrands()
    {
        Models = new Collection<CarModel>();
    }
}