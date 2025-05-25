using AutoMapper;
using Backend.Controllers.Resources;
using Backend.Models;
using Backend.Persistence;
using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Backend.Controllers;

[Route("/vehicle/{vehicleId}/photos")]
public class PhotoController: Controller
{
    private readonly int MAX_FILE_SIZE = 10 * 1024 * 1024;
    private readonly string[] ACCEPTED_FILE_TYPES = ["jpg", "jpeg", "png"];
    
    private readonly IHostingEnvironment _host;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IVehicleRepository _vehicleRepository;
    public PhotoController(IHostingEnvironment host, IUnitOfWork unitOfWork, IMapper mapper, IVehicleRepository vehicleRepository)
    {
        _host = host;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _vehicleRepository = vehicleRepository;
    }
    
    [HttpPost]
    public async Task<IActionResult> Upload(int vehicleId, IFormFile file)
    {
        var vehicle = await _vehicleRepository.GetVehicleById(vehicleId, includeRelated: false);
        if (vehicle == null)
            return NotFound();
        
        if (file == null || file.Length == 0)
            return BadRequest("File is empty");
        if (file.Length > MAX_FILE_SIZE)
            return BadRequest("Maximum file size is 10MB");
        if (ACCEPTED_FILE_TYPES.Contains(Path.GetExtension(file.FileName).ToLower()))
            return BadRequest("Invalid file type");
        
        var uploadsFolderPath = Path.Combine(_host.WebRootPath, "uploads");
        if (!Directory.Exists(uploadsFolderPath))
            Directory.CreateDirectory(uploadsFolderPath);
        
        var fileName = $"{Guid.NewGuid()}{file.FileName}{Path.GetExtension(file.FileName)}";
        var filePath = Path.Combine(uploadsFolderPath, fileName);

        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
        
        var photo = new Photo {FileName = fileName};
        vehicle.Photos.Add(photo);
        await _unitOfWork.CompleteAsync();
        
        return Ok(_mapper.Map<Photo, PhotoResource>(photo));
        return Ok();
    }
}