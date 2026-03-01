using AutoCita.Abstractions;
using AutoCita.Enums;

namespace AutoCita.Models
{
    internal class Cliente : Persona
    {
        public string Direccion;
        public List<Cita> HistorialCitas;
        public List<Vehiculo> ListadoVehiculos;

        public Cliente(string id, string nombre, string apellido, string telefono, string email, DateOnly fechaNacimiento, string numeroDocumento, TipoDocumento tipoDocumento, string direccion) : base(id, nombre, apellido, telefono, email, fechaNacimiento, numeroDocumento, tipoDocumento)
        {
            Direccion = direccion;
            HistorialCitas = [];
            ListadoVehiculos = [];
        }

        public bool CrearCliente()
        {
            // Lógica para crear un nuevo cliente en la base de datos
            return true;
        }

        public bool ActualizarCliente(string? telefono, string? email, string? direccion)
        {
            // Lógica para actualizar la información del cliente en la base de datos
            return true;
        }

        public Cita AgendarCita(Vehiculo vehiculo, DateTime fechaHora) 
        {
            // Lógica para agendar una nueva cita para el cliente
            return new Cita();
        }
    }
}
