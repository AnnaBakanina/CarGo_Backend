using System.Collections.ObjectModel;

namespace Backend.Controllers.Resources;

public class CarBrandResource
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<CarModelResource> Models { get; set; }

    public CarBrandResource()
    {
        Models = new Collection<CarModelResource>();
    }
}