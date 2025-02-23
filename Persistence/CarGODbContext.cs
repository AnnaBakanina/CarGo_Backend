using Microsoft.EntityFrameworkCore;

namespace Backend.Persistence;

public class CarGODbContext : DbContext
{
    public CarGODbContext(DbContextOptions<CarGODbContext> options) : base(options)
    {
        
    }
}