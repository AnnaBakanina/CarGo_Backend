using AutoMapper;
using Backend.Controllers.Resources;
using Backend.Models;
using Backend.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("/vehicles")]
public class VehicleController: ControllerBase
{
    private readonly IMapper _mapper;
    private readonly CarGoDbContext _context;

    public VehicleController(IMapper mapper, CarGoDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateVehicle([FromBody] VehicleResource vehicleResource)
    {
        var vehicle = _mapper.Map<VehicleResource, Vehicle>(vehicleResource);
        _context.Vehicles.Add(vehicle);
        await _context.SaveChangesAsync();
        var result = _mapper.Map<Vehicle, VehicleResource>(vehicle);
        return Ok(result);
    }
}