using AutoMapper;
using Backend.Controllers.Resources;
using Backend.Models;
using Backend.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[Route("/vehicles")]
public class VehicleController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly CarGoDbContext _context;
    private readonly IVehicleRepository _vehicleRepository;

    public VehicleController(IMapper mapper, CarGoDbContext context, IVehicleRepository vehicleRepository)
    {
        _mapper = mapper;
        _context = context;
        _vehicleRepository = vehicleRepository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleResource saveVehicleResource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var vehicle = _mapper.Map<SaveVehicleResource, Vehicle>(saveVehicleResource);
        _context.Vehicles.Add(vehicle);
        await _context.SaveChangesAsync();
        
        vehicle = await _vehicleRepository.GetVehicleById(vehicle.Id);
        var result = _mapper.Map<Vehicle, VehicleResource>(vehicle);
        return Ok(result);
    }

    // TODO: Test
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateVehicle(int id, [FromBody] SaveVehicleResource saveVehicleResource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var vehicle = await _context.Vehicles.FindAsync(id);

        if (vehicle == null)
            return NotFound();

        _mapper.Map(saveVehicleResource, vehicle);
        await _context.SaveChangesAsync();
        
        vehicle = await _vehicleRepository.GetVehicleById(id);
        var result = _mapper.Map<Vehicle, VehicleResource>(vehicle);
        return Ok(result);
    }

    // TODO: Test
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteVehicle(int id)
    {
        var vehicle = await _context.Vehicles.FindAsync(id);

        if (vehicle == null)
            return NotFound();

        _context.Vehicles.Remove(vehicle);
        await _context.SaveChangesAsync();
        return Ok(id);
    }

    // TODO: Test
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetVehicle(int id)
    {
        var vehicle = await _vehicleRepository.GetVehicleById(id);

        var vehicleResource = _mapper.Map<Vehicle, VehicleResource>(vehicle);
        return Ok(vehicleResource);
    }
}