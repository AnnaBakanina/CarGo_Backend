using AutoMapper;
using Backend.Controllers.Resources;
using Backend.Models;
using Backend.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

public class CarBrandController: Controller
{
    private readonly CarGoDbContext _dbContext;
    private readonly IMapper _mapper;

    public CarBrandController(CarGoDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    [HttpGet("/car-brands")]
    public async Task<IEnumerable<CarBrandResource>> GetBrands()
    {
        var brands = await _dbContext.Brands.Include(m => m.CarModel).ToListAsync();
        return _mapper.Map<IEnumerable<CarBrandResource>>(brands);
    }
}