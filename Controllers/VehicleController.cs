using AutoMapper;
using Backend.Controllers.Resources;
using Backend.Models;
using Backend.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("/vehicles")]
public class VehicleController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public VehicleController(IMapper mapper, IVehicleRepository vehicleRepository, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _vehicleRepository = vehicleRepository;
        _unitOfWork = unitOfWork;
    }

    [HttpPost]
    public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleResource saveVehicleResource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var vehicle = _mapper.Map<SaveVehicleResource, Vehicle>(saveVehicleResource);
        _vehicleRepository.AddVehicle(vehicle);
        await _unitOfWork.CompleteAsync();
        
        vehicle = await _vehicleRepository.GetVehicleById(vehicle.Id);
        var result = _mapper.Map<Vehicle, VehicleResource>(vehicle);
        return Ok(result);
    }

    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<IActionResult> UpdateVehicle(int id, [FromBody] SaveVehicleResource saveVehicleResource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var vehicle = await _vehicleRepository.GetVehicleById(id, includeRelated: false);

        _mapper.Map(saveVehicleResource, vehicle);
        await _unitOfWork.CompleteAsync();
        
        vehicle = await _vehicleRepository.GetVehicleById(id);
        var result = _mapper.Map<Vehicle, VehicleResource>(vehicle);
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> DeleteVehicle(int id)
    {
        var vehicle = await _vehicleRepository.GetVehicleById(id, includeRelated: false);
        
        _vehicleRepository.RemoveVehicle(vehicle);
        await _unitOfWork.CompleteAsync();
        return Ok(id);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetVehicle(int id)
    {
        var vehicle = await _vehicleRepository.GetVehicleById(id);

        var vehicleResource = _mapper.Map<Vehicle, VehicleResource>(vehicle);
        return Ok(vehicleResource);
    }
    
    [HttpGet("")]
    public async Task<QueryResultResource<VehicleResource>> GetAllVehicle(VehicleQueryResource vehicleQueryResource)
    {
        var filter = _mapper.Map<VehicleQueryResource, VehicleQuery>(vehicleQueryResource);
        var queryResult = await _vehicleRepository.GetVehicles(filter);

        return _mapper.Map<QueryResult<Vehicle>, QueryResultResource<VehicleResource>>(queryResult);
    }
}