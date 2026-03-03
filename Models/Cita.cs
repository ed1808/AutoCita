using System.Text.Json.Serialization;
using AutoCita.Enums;

namespace AutoCita.Models
{
    internal class Cita
    {
        public Guid Id { get; set; }
        public Guid ClienteId { get; set; }
        public Guid VehiculoId { get; set; }
        public string MotivoSolicitud { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public int DuracionMinutos { get; set; }
        public DateTime FechaHoraFin { get; set; }
        public EstadoCita Estado { get; set; }
        public string Observaciones { get; set; }

        [JsonConstructor]
        public Cita(Guid id, Guid clienteId, Guid vehiculoId, string motivoSolicitud, DateTime fechaHoraInicio, int duracionMinutos, EstadoCita estado, string observaciones)
        {
            Id = id;
            ClienteId = clienteId;
            VehiculoId = vehiculoId;
            MotivoSolicitud = motivoSolicitud;
            FechaHoraInicio = fechaHoraInicio;
            DuracionMinutos = duracionMinutos;
            FechaHoraFin = CalcularFechaHoraFin();
            Estado = estado;
            Observaciones = observaciones;
        }

        public DateTime CalcularFechaHoraFin()
        {
            return FechaHoraInicio.AddMinutes(DuracionMinutos);
        }

        public void Reprogramar(DateTime nuevaFechaHoraInicio)
        {
            FechaHoraInicio = nuevaFechaHoraInicio;
            FechaHoraFin = CalcularFechaHoraFin();
        }

        public void ActualizarEstado(EstadoCita nuevoEstado)
        {
            Estado = nuevoEstado;
        }

        public void Cancelar(string motivoCancelacion)
        {
            Estado = EstadoCita.CANCELADA;
            Observaciones += $" Cancelada: {motivoCancelacion} [{DateTime.Now}]";
        }
    }
}
