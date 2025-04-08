using AutoMapper;
using Backend.Controllers.Resources;
using Backend.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

public class CarTypeController: Controller
{
    private readonly CarGoDbContext _dbContext;
    private readonly IMapper _mapper;
    
    public CarTypeController(CarGoDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    [HttpGet("/car-types")]
    public async Task<IEnumerable<CarTypeResource>> GetCarTypes()
    {
        var carTypes = await _dbContext.CarTypes.ToListAsync();
        return _mapper.Map<IEnumerable<CarTypeResource>>(carTypes);
    }
}