using AutoMapper;
using Backend.Controllers.Resources;
using Backend.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

public class AdvertisementStatusController: Controller
{
    private readonly CarGoDbContext _dbContext;
    private readonly IMapper _mapper;

    public AdvertisementStatusController(CarGoDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    [HttpGet("/advertisement-statuses")]
    public async Task<IEnumerable<KeyValuePairResource>> GetAdvertisementStatuses()
    {
        var advertisementStatuses = await _dbContext.AdvertisementStatuses.ToListAsync();
        return _mapper.Map<IEnumerable<KeyValuePairResource>>(advertisementStatuses);
    }
}