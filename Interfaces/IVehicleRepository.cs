using AutoCita.Abstractions;

namespace AutoCita.Interfaces
{
    internal interface IVehicleRepository
    {
        Task<bool> AddVehicle(Vehiculo vehicle);
        Task<List<Vehiculo>> GetVehicles();
        Task<Vehiculo?> GetVehicle(Guid id);
        Task<Vehiculo?> GetVehicleByPlate(string plate);
        Task<List<Vehiculo>> GetVehiclesByCustomer(Guid customerId);
        Task<bool> UpdateVehicle(Vehiculo vehicle);
    }
}
