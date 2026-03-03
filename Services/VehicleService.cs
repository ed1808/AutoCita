using AutoCita.Abstractions;
using AutoCita.Interfaces;

namespace AutoCita.Services
{
    internal class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ICustomerRepository _customerRepository;

        public VehicleService(IVehicleRepository vehicleRepository, ICustomerRepository customerRepository)
        {
            _vehicleRepository = vehicleRepository;
            _customerRepository = customerRepository;
        }

        public async Task<bool> RegistrarVehiculo(Vehiculo vehicle)
        {
            if (!vehicle.ValidarInformacion())
            {
                return false;
            }

            // Verificar que el propietario existe
            var propietario = await _customerRepository.GetCustomer(vehicle.PropietarioId);

            if (propietario is null)
            {
                return false;
            }

            // Verificar que no exista un vehículo con la misma placa
            var existente = await _vehicleRepository.GetVehicleByPlate(vehicle.Placa);

            if (existente is not null)
            {
                return false;
            }

            return await _vehicleRepository.AddVehicle(vehicle);
        }

        public async Task<List<Vehiculo>> ObtenerVehiculos()
        {
            return await _vehicleRepository.GetVehicles();
        }

        public async Task<Vehiculo?> ObtenerVehiculoPorId(Guid id)
        {
            return await _vehicleRepository.GetVehicle(id);
        }

        public async Task<Vehiculo?> ObtenerVehiculoPorPlaca(string placa)
        {
            return await _vehicleRepository.GetVehicleByPlate(placa);
        }

        public async Task<List<Vehiculo>> ObtenerVehiculosPorCliente(Guid clienteId)
        {
            return await _vehicleRepository.GetVehiclesByCustomer(clienteId);
        }

        public async Task<bool> ActualizarVehiculo(Vehiculo vehicle)
        {
            if (!vehicle.ValidarInformacion())
            {
                return false;
            }

            var existente = await _vehicleRepository.GetVehicle(vehicle.Id);

            if (existente is null)
            {
                return false;
            }

            return await _vehicleRepository.UpdateVehicle(vehicle);
        }
    }
}
