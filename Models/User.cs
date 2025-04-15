using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

[Table("Users")]
public class User
{
    public int Id { get; set; }
    [Required]
    [StringLength(255)]
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [Required]
    [StringLength(255)]
    public string Email { get; set; }
    [Required]
    [StringLength(255)]
    public string Password { get; set; }
    [Required]
    [StringLength(255)]
    public string PhoneNumber { get; set; }
    public DateTime LastUpdated { get; set; }
    public ICollection<Vehicle> Vehicles { get; set; }

    public User()
    {
        Vehicles = new Collection<Vehicle>();
    }
}