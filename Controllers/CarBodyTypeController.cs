using AutoMapper;
using Backend.Controllers.Resources;
using Backend.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

public class CarBodyTypeController: Controller
{
    private readonly CarGoDbContext _dbContext;
    private readonly IMapper _mapper;
    
    public CarBodyTypeController(CarGoDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    [HttpGet("/car-body-types")]
    public async Task<IEnumerable<CarBodyTypeResource>> GetBrands()
    {
        var brands = await _dbContext.Brands.Include(m => m.CarModel).ToListAsync();
        return _mapper.Map<IEnumerable<CarBodyTypeResource>>(brands);
    }
}