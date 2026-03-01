namespace AutoCita.Models
{
    internal class Agenda
    {
        public string Id;
        public string Nombre;
        public string Observaciones;
        public DateOnly FechaCreacion;

        public Agenda(string id, string nombre, string observaciones, DateOnly fechaCreacion)
        {
            Id = id;
            Nombre = nombre;
            Observaciones = observaciones;
            FechaCreacion = fechaCreacion;
        }

        public bool ValidarDisponibilidad(DateTime fechaHoraInicio, int duracionMinutos)
        {
            // Lógica para validar la disponibilidad de la agenda en el horario solicitado
            return true;
        }

        public void AgendarCita(Cita cita)
        {
            // Lógica para agregar una cita a la agenda
        }

        public void CancelarCita(Cita cita, string motivo)
        {
            // Lógica para cancelar una cita en la agenda
        }

        public void ReprogramarCita(Cita cita, DateTime nuevoInicio)
        {
            // Lógica para reprogramar una cita en la agenda
        }

        public List<Cita> ListarCitasPorDia(DateOnly fecha)
        {
            // Lógica para listar las citas programadas para un día específico
            return new List<Cita>();
        }
    }
}
