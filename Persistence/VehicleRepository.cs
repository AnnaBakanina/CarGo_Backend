using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Persistence;

public class VehicleRepository : IVehicleRepository
{
    private readonly CarGoDbContext _context;

    public VehicleRepository(CarGoDbContext context)
    {
        _context = context;
    }

    public async Task<Vehicle> GetVehicleById(int id, bool includeRelated = true)
    {
        if (!includeRelated)
            return await _context.Vehicles.FindAsync(id);
        
        return await _context.Vehicles
            .Include(v => v.Model)
            .Include(v => v.CarType)
            .Include(v=> v.TechState)
            .Include(v=> v.User)
            .Include(v=>v.Model)
            .ThenInclude(m=> m.Brand)
            .SingleOrDefaultAsync(v => v.Id == id) ?? throw new InvalidOperationException();
    }

    public void AddVehicle(Vehicle vehicle)
    {
        _context.Vehicles.Add(vehicle);
    }
    
    public void RemoveVehicle(Vehicle vehicle)
    {
        _context.Vehicles.Remove(vehicle);
    }
}