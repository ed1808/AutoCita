using AutoCita.Abstractions;

namespace AutoCita.Interfaces
{
    internal interface IVehicleRepository
    {
        bool AddVehicle(Vehiculo vehicle);
        List<Vehiculo> GetVehicles();
        Vehiculo? GetVehicle(string plate);
        bool UpdateVehicle(Vehiculo vehicle);
    }
}
