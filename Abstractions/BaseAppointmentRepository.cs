using AutoCita.Interfaces;
using AutoCita.Models;

namespace AutoCita.Abstractions
{
    internal abstract class BaseAppointmentRepository : IAppointmentRepository
    {
        protected readonly string _dataSource;

        protected BaseAppointmentRepository(string dataSource)
        {
            _dataSource = dataSource;
        }

        public async Task<bool> AddAppointment(Cita appointment)
        {
            return await PersistNewAppointment(appointment);
        }

        public async Task<Cita?> GetAppointment(Guid id)
        {
            return await LoadAppointmentById(id);
        }

        public async Task<List<Cita>> GetAppointments()
        {
            return await LoadAllAppointments();
        }

        public async Task<List<Cita>> GetAppointmentsByCustomer(Guid customerId)
        {
            return await LoadAppointmentsByCustomer(customerId);
        }

        public async Task<List<Cita>> GetAppointmentsByVehicle(Guid vehicleId)
        {
            return await LoadAppointmentsByVehicle(vehicleId);
        }

        public async Task<bool> UpdateAppointment(Cita appointment)
        {
            return await PersistUpdatedAppointment(appointment);
        }

        protected abstract Task<bool> PersistNewAppointment(Cita appointment);
        protected abstract Task<List<Cita>> LoadAllAppointments();
        protected abstract Task<Cita?> LoadAppointmentById(Guid id);
        protected abstract Task<List<Cita>> LoadAppointmentsByCustomer(Guid customerId);
        protected abstract Task<List<Cita>> LoadAppointmentsByVehicle(Guid vehicleId);
        protected abstract Task<bool> PersistUpdatedAppointment(Cita appointment);
    }
}
