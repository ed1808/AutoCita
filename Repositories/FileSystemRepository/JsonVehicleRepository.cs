using AutoCita.Abstractions;
using AutoCita.FileHandler;

namespace AutoCita.Repositories.FileSystemRepository
{
    internal class JsonVehicleRepository : BaseVehicleRepository
    {
        public JsonVehicleRepository() : base(Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "AutoCita",
            "Data",
            "vehiculos.json"
        ))
        {
        }

        protected override async Task<List<Vehiculo>> LoadAllVehicles()
        {
            return await FileHandler<List<Vehiculo>>.GetData(_dataSource) ?? [];
        }

        protected override async Task<Vehiculo?> LoadVehicleById(Guid id)
        {
            var vehicles = await LoadAllVehicles();
            return vehicles.FirstOrDefault(v => v.Id == id);
        }

        protected override async Task<Vehiculo?> LoadVehicleByPlate(string plate)
        {
            var vehicles = await LoadAllVehicles();
            return vehicles.FirstOrDefault(v => v.Placa.Equals(plate, StringComparison.OrdinalIgnoreCase));
        }

        protected override async Task<List<Vehiculo>> LoadVehiclesByCustomer(Guid customerId)
        {
            var vehicles = await LoadAllVehicles();
            return vehicles.Where(v => v.PropietarioId == customerId).ToList();
        }

        protected override async Task<bool> PersistNewVehicle(Vehiculo vehicle)
        {
            var vehicles = await LoadAllVehicles();
            vehicles.Add(vehicle);
            return await FileHandler<List<Vehiculo>>.SaveData(_dataSource, vehicles);
        }

        protected override async Task<bool> PersistUpdatedVehicle(Vehiculo vehicle)
        {
            var vehicles = await LoadAllVehicles();
            var index = vehicles.FindIndex(v => v.Id == vehicle.Id);

            if (index < 0)
            {
                return false;
            }

            vehicles[index] = vehicle;
            return await FileHandler<List<Vehiculo>>.SaveData(_dataSource, vehicles);
        }
    }
}
