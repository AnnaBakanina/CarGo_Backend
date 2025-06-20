using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Persistence;

public class PhotoRepository : IPhotoRepository
{
    private readonly CarGoDbContext _context;

    public PhotoRepository(CarGoDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Photo>> GetPhotos(int vehicleId)
    {
        return await _context.Photos
            .Where(p => p.VehicleId == vehicleId)
            .ToListAsync();
    }
}