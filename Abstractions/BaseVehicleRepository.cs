using AutoCita.Interfaces;

namespace AutoCita.Abstractions
{
    internal abstract class BaseVehicleRepository : IVehicleRepository
    {
        protected readonly string _dataSource;

        protected BaseVehicleRepository(string dataSource)
        {
            _dataSource = dataSource;
        }

        public async Task<bool> AddVehicle(Vehiculo vehicle)
        {
            if (!vehicle.ValidarInformacion())
            {
                return false;
            }

            return await PersistNewVehicle(vehicle);
        }

        public async Task<Vehiculo?> GetVehicle(Guid id)
        {
            return await LoadVehicleById(id);
        }

        public async Task<Vehiculo?> GetVehicleByPlate(string plate)
        {
            return await LoadVehicleByPlate(plate);
        }

        public async Task<List<Vehiculo>> GetVehicles()
        {
            return await LoadAllVehicles();
        }

        public async Task<List<Vehiculo>> GetVehiclesByCustomer(Guid customerId)
        {
            return await LoadVehiclesByCustomer(customerId);
        }

        public async Task<bool> UpdateVehicle(Vehiculo vehicle)
        {
            if (!vehicle.ValidarInformacion())
            {
                return false;
            }

            return await PersistUpdatedVehicle(vehicle);
        }

        protected abstract Task<bool> PersistNewVehicle(Vehiculo vehicle);
        protected abstract Task<List<Vehiculo>> LoadAllVehicles();
        protected abstract Task<Vehiculo?> LoadVehicleById(Guid id);
        protected abstract Task<Vehiculo?> LoadVehicleByPlate(string plate);
        protected abstract Task<List<Vehiculo>> LoadVehiclesByCustomer(Guid customerId);
        protected abstract Task<bool> PersistUpdatedVehicle(Vehiculo vehicle);
    }
}
