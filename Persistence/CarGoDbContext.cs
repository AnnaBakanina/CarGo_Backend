using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Persistence;

public class CarGoDbContext : DbContext
{
    public CarGoDbContext(DbContextOptions<CarGoDbContext> options) : base(options) { } 
    public DbSet<CarBrands> Brands { get; set; }
}