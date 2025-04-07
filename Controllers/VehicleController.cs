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

    public VehicleController(IMapper mapper, CarGoDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleResource saveVehicleResource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var vehicle = _mapper.Map<SaveVehicleResource, Vehicle>(saveVehicleResource);
        _context.Vehicles.Add(vehicle);
        await _context.SaveChangesAsync();
        var result = _mapper.Map<Vehicle, SaveVehicleResource>(vehicle);
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

        _mapper.Map<SaveVehicleResource, Vehicle>(saveVehicleResource, vehicle);

        await _context.SaveChangesAsync();
        var result = _mapper.Map<Vehicle, SaveVehicleResource>(vehicle);
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
        // var vehicle = await _context.Vehicles.FindAsync(id);
        var vehicle = await _context.Vehicles
            .Include(v => v.Model)
            .Include(v => v.CarType)
            .Include(v=> v.TechState)
            .Include(v=> v.User)
            .Include(v=>v.Model)
            .ThenInclude(m=> m.Brand)
            .SingleOrDefaultAsync(v => v.Id == id);

        if (vehicle == null)
            return NotFound();

        var vehicleResource = _mapper.Map<Vehicle, VehicleResource>(vehicle);
        return Ok(vehicleResource);
    }
}