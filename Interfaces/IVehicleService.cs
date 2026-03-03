using AutoCita.Abstractions;

namespace AutoCita.Interfaces
{
    internal interface IVehicleService
    {
        Task<bool> RegistrarVehiculo(Vehiculo vehicle);
        Task<List<Vehiculo>> ObtenerVehiculos();
        Task<Vehiculo?> ObtenerVehiculoPorId(Guid id);
        Task<Vehiculo?> ObtenerVehiculoPorPlaca(string placa);
        Task<List<Vehiculo>> ObtenerVehiculosPorCliente(Guid clienteId);
        Task<bool> ActualizarVehiculo(Vehiculo vehicle);
    }
}
