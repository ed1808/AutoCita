using AutoCita.Models;

namespace AutoCita.Interfaces
{
    internal interface IAppointmentRepository
    {
        bool AddAppointment(Cita appointment);
        List<Cita> GetAppointments();
        Cita? GetAppointment(string id);
        bool UpdateAppointment(Cita appointment);
    }
}
