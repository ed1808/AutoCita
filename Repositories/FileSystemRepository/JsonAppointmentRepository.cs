using AutoCita.Abstractions;
using AutoCita.FileHandler;
using AutoCita.Models;

namespace AutoCita.Repositories.FileSystemRepository
{
    internal class JsonAppointmentRepository : BaseAppointmentRepository
    {
        public JsonAppointmentRepository() : base(Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "AutoCita",
            "Data",
            "citas.json"
        ))
        {
        }

        protected override async Task<List<Cita>> LoadAllAppointments()
        {
            return await FileHandler<List<Cita>>.GetData(_dataSource) ?? [];
        }

        protected override async Task<Cita?> LoadAppointmentById(Guid id)
        {
            var appointments = await LoadAllAppointments();
            return appointments.FirstOrDefault(a => a.Id == id);
        }

        protected override async Task<List<Cita>> LoadAppointmentsByCustomer(Guid customerId)
        {
            var appointments = await LoadAllAppointments();
            return appointments.Where(a => a.ClienteId == customerId).ToList();
        }

        protected override async Task<List<Cita>> LoadAppointmentsByVehicle(Guid vehicleId)
        {
            var appointments = await LoadAllAppointments();
            return appointments.Where(a => a.VehiculoId == vehicleId).ToList();
        }

        protected override async Task<bool> PersistNewAppointment(Cita appointment)
        {
            var appointments = await LoadAllAppointments();
            appointments.Add(appointment);
            return await FileHandler<List<Cita>>.SaveData(_dataSource, appointments);
        }

        protected override async Task<bool> PersistUpdatedAppointment(Cita appointment)
        {
            var appointments = await LoadAllAppointments();
            var index = appointments.FindIndex(a => a.Id == appointment.Id);

            if (index < 0)
            {
                return false;
            }

            appointments[index] = appointment;
            return await FileHandler<List<Cita>>.SaveData(_dataSource, appointments);
        }
    }
}

