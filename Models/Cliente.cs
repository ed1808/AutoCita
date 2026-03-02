using AutoCita.Abstractions;
using AutoCita.Enums;

namespace AutoCita.Models
{
    internal class Cliente : Persona
    {
        public string Direccion;

        public Cliente(Guid id, string nombre, string apellido, string telefono, string email, DateOnly fechaNacimiento, string numeroDocumento, 
            TipoDocumento tipoDocumento, string direccion) 
            : base(id, nombre, apellido, telefono, email, fechaNacimiento, numeroDocumento, tipoDocumento)
        {
            Direccion = direccion;
        }

        public override bool ValidarInformacion()
        {
            if (
                string.IsNullOrWhiteSpace(Nombre) || string.IsNullOrWhiteSpace(Apellido) || string.IsNullOrWhiteSpace(Telefono) || 
                string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(NumeroDocumento)
            )
            {
                return false;
            }

            return true;
        }
    }
}
