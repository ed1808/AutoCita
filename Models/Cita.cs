using AutoCita.Enums;

namespace AutoCita.Models
{
    internal class Cita
    {
        public Guid Id;
        public string MotivoSolicitud;
        public DateTime FechaHoraInicio;
        public int DuracionMinutos;
        public DateTime FechaHoraFin;
        public EstadoCita Estado;
        public string Observaciones;

        public Cita(Guid id, string motivoSolicitud, DateTime fechaHoraInicio, int duracionMinutos, EstadoCita estado, string observaciones)
        {
            Id = id;
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

        public void Cancelar(string motivoCancelacion)
        {
            Estado = EstadoCita.CANCELADA;
            Observaciones += $" Cancelada: {motivoCancelacion} [{DateTime.Now}]";
        }
    }
}
