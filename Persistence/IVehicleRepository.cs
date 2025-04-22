using Backend.Models;

namespace Backend.Persistence;

public interface IVehicleRepository
{
    Task<Vehicle> GetVehicleById(int id, bool includeRelated = true);
    Task<List<Vehicle>> GetVehicles(VehicleQuery vehicleQuery, bool includeRelated = true);
    void AddVehicle(Vehicle vehicle);
    void RemoveVehicle(Vehicle vehicle);
}