using Backend.Models;

namespace Backend.Persistence;

public interface IPhotoRepository
{
    Task<IEnumerable<Photo>> GetPhotos(int vehicleId);
}