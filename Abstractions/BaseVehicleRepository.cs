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

        public bool AddVehicle(Vehiculo vehicle)
        {
            return SaveVehicleToDataSource(vehicle);
        }

        public Vehiculo? GetVehicle(string plate)
        {
            GetVehicleFromDataSource(plate);

            return null;
        }

        public List<Vehiculo> GetVehicles()
        {
            GetVehiclesFromDataSource();

            return [];
        }

        public bool UpdateVehicle(Vehiculo vehicle)
        {
            return UpdateVehicleInDataSource(vehicle);
        }

        protected abstract bool SaveVehicleToDataSource(Vehiculo vehicle);
        protected abstract void GetVehiclesFromDataSource();
        protected abstract void GetVehicleFromDataSource(string plate);
        protected abstract bool UpdateVehicleInDataSource(Vehiculo vehicle);
    }
}
