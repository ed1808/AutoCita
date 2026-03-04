using AutoCita.Enums;
using AutoCita.Models;

namespace AutoCita.Interfaces
{
    internal interface IAppointmentService
    {
        Task<bool> AgendarCita(Cita appointment);
        Task<List<Cita>> ObtenerCitas();
        Task<Cita?> ObtenerCitaPorId(Guid id);
        Task<List<Cita>> ObtenerCitasPorCliente(Guid clienteId);
        Task<List<Cita>> ObtenerCitasPorVehiculo(Guid vehiculoId);
        Task<bool> ActualizarCita(Cita appointment);
        Task<bool> CancelarCita(Guid id, string motivoCancelacion);
        Task<bool> ReprogramarCita(Guid id, DateTime nuevaFechaHoraInicio);
        Task<bool> ActualizarEstadoCita(Guid id, EstadoCita nuevoEstado);
    }
}
