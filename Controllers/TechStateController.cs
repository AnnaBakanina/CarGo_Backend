using AutoMapper;
using Backend.Controllers.Resources;
using Backend.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

public class TechStateController : Controller
{
    private readonly CarGoDbContext _dbContext;
    private readonly IMapper _mapper;
    
    public TechStateController(CarGoDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    [HttpGet("/car-tech-state")]
    public async Task<IEnumerable<TechStateResource>> GetTechSates()
    {
        var techStates = await _dbContext.TechStates.ToListAsync();
        return _mapper.Map<IEnumerable<TechStateResource>>(techStates);
    }
}