using Backend.Models;
using Backend.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

public class CarBrandController: Controller
{
    private readonly CarGODbContext _dbContext;

    public CarBrandController(CarGODbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    [HttpGet("/car-brands")]
    public IEnumerable<CarBrands> GetBrands()
    {
        return null;
        // return _dbContext.CarBrands.Include(m => m.CarModels).ToList <- check if only ToListAsync can be used. Then:
    }
    
    // public async Task<IEnumerable<CarBrands>> GetBrends()
    // {
    //     return await _dbContext.CarBrands.Include(m => m.CarModels).ToListAsync();
    // }
}