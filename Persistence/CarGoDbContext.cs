using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Persistence;

public class CarGoDbContext : DbContext
{
    public CarGoDbContext(DbContextOptions<CarGoDbContext> options) : base(options) { }
    
    public DbSet<CarBrands> Brands { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<CarModel> CarModels { get; set; }
    public DbSet<CarType> CarTypes { get; set; }
    public DbSet<TechState> TechStates { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<City> City { get; set; }
    public DbSet<AdvertisementStatus> AdvertisementStatuses { get; set; }
}