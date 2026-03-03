using System.Text.Json.Serialization;
using AutoCita.Abstractions;
using AutoCita.Enums;

namespace AutoCita.Models
{
    internal class Cliente : Persona
    {
        public string Direccion { get; set; }

        [JsonConstructor]
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

        public void ActualizarInformacion(string telefono, string email, string numeroDocumento, TipoDocumento tipoDocumento, string direccion)
        {
            Telefono = telefono;
            Email = email;
            NumeroDocumento = numeroDocumento;
            TipoDocumento = tipoDocumento;
            Direccion = direccion;
        }
    }
}
