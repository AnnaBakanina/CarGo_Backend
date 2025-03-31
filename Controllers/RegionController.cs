using AutoMapper;
using Backend.Controllers.Resources;
using Backend.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

public class RegionController : Controller
{
    private readonly CarGoDbContext _dbContext;
    private readonly IMapper _mapper;

    public RegionController(CarGoDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    [HttpGet("/regions")]
    public async Task<IEnumerable<RegionResource>> GetRegions()
    {
        var regions = await _dbContext.Regions.Include(x => x.City).ToListAsync();
        return _mapper.Map<IEnumerable<RegionResource>>(regions);
    }
}