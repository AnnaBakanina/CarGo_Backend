using System.Collections.ObjectModel;

namespace Backend.Models;

public class CarBrands
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<CarModel> Models { get; set; }

    public CarBrands()
    {
        Models = new Collection<CarModel>();
    }
}