using System.Collections.ObjectModel;
using Backend.Models;

namespace Backend.Controllers.Resources;

public class RegionResource
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<KeyValuePairResource> City { get; set; }

    public RegionResource()
    {
        City = new Collection<KeyValuePairResource>();
    }
}