using System.Collections.ObjectModel;

namespace Backend.Models;

public class Region
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<City> City { get; set; }

    public Region()
    {
        City = new Collection<City>();
    }
}