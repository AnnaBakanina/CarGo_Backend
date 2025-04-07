using Backend.Models;

namespace Backend.Persistence;

public interface IVehicleRepository
{
    Task<Vehicle> GetVehicleById(int id);
}