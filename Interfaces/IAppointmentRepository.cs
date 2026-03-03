using AutoCita.Models;

namespace AutoCita.Interfaces
{
    internal interface IAppointmentRepository
    {
        Task<bool> AddAppointment(Cita appointment);
        Task<List<Cita>> GetAppointments();
        Task<Cita?> GetAppointment(Guid id);
        Task<List<Cita>> GetAppointmentsByCustomer(Guid customerId);
        Task<List<Cita>> GetAppointmentsByVehicle(Guid vehicleId);
        Task<bool> UpdateAppointment(Cita appointment);
    }
}
