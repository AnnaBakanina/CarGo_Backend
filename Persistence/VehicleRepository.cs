using System.Linq.Expressions;
using Backend.Extensions;
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
            .Include(v => v.CarType)
            .Include(v=> v.TechState)
            .Include(v => v.City)
            .ThenInclude(c=> c.Region)
            .Include(s=> s.AdvertisementStatus)
            .Include(v=>v.Model)
            .ThenInclude(m=> m.Brand)
            .SingleOrDefaultAsync(v => v.Id == id) ?? throw new InvalidOperationException();
    }

    public async Task<QueryResult<Vehicle>> GetVehicles(VehicleQuery vehicleQuery, bool includeRelated = true)
    {
        // if (!includeRelated)
        //     return await _context.Vehicles.ToListAsync();
        
        var queryResult = new QueryResult<Vehicle>();

        var query = _context.Vehicles
            .Include(v => v.City)
            .Include(v => v.CarType)
            .Include(v => v.TechState)
            .Include(s => s.AdvertisementStatus)
            .Include(v => v.Model)
            .ThenInclude(m => m.Brand).AsQueryable();
        
        if (vehicleQuery.BrandId.HasValue)
            query = query.Where(v => v.Model.BrandId == vehicleQuery.BrandId.Value);
        
        if (vehicleQuery.ModelId.HasValue)
            query = query.Where(v => v.ModelId == vehicleQuery.ModelId.Value);
        
        var orderByExpressions = new Dictionary<string, Expression<Func<Vehicle, object>>>()
        {
            ["price"] = v => v.Price,
            ["lastUpdated"] = v => v.LastUpdated
        };
        query = query.ApplyOrdering(vehicleQuery, orderByExpressions);
        queryResult.TotalItems = await query.CountAsync();
        query = query.ApplyPaging(vehicleQuery);
        
        queryResult.Data = await query.ToListAsync();
        return queryResult;
    }

    public void AddVehicle(Vehicle vehicle)
    {
        _context.Vehicles.Add(vehicle);
    }
    
    public void RemoveVehicle(Vehicle vehicle)
    {
        _context.Vehicles.Remove(vehicle);
    }

    private IQueryable<Vehicle> ApplyOrdering(VehicleQuery vehicleQuery, IQueryable<Vehicle> query, Dictionary<string, Expression<Func<Vehicle, object>>> orderByExpressions)
    {
        return vehicleQuery.IsSortAscending ? query.OrderBy(orderByExpressions[vehicleQuery.SortBy]) : query.OrderByDescending(orderByExpressions[vehicleQuery.SortBy]);
    }
}