using AutoCita.Abstractions;

namespace AutoCita.Repositories.FileSystemRepository
{
    internal class JsonVehicleRepository : BaseVehicleRepository
    {
        public JsonVehicleRepository() : base(Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "AutoCita",
            "Vehiculos",
            "index.json"
        ))
        {
        }

        protected override void GetVehicleFromDataSource(string plate)
        {
            throw new NotImplementedException();
        }

        protected override void GetVehiclesFromDataSource()
        {
            throw new NotImplementedException();
        }

        protected override bool SaveVehicleToDataSource(Vehiculo vehicle)
        {
            throw new NotImplementedException();
        }

        protected override bool UpdateVehicleInDataSource(Vehiculo vehicle)
        {
            throw new NotImplementedException();
        }
    }
}
