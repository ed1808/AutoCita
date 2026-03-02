using AutoCita.Abstractions;
using AutoCita.Models;

namespace AutoCita.Repositories.FileSystemRepository
{
    internal class JsonAppointmentRepository : BaseAppointmentRepository
    {
        public JsonAppointmentRepository() : base(Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "AutoCita",
            "Citas",
            "index.json"
        ))
        {
        }

        protected override void GetAppointmentFromDataSource(string id)
        {
            throw new NotImplementedException();
        }

        protected override void GetAppointmentsFromDataSource()
        {
            throw new NotImplementedException();
        }

        protected override bool SaveAppointmentToDataSource(Cita appointment)
        {
            throw new NotImplementedException();
        }

        protected override bool UpdateAppointmentFromDataSource(Cita appointment)
        {
            throw new NotImplementedException();
        }
    }
}

