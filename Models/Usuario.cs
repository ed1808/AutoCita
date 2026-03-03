using System.Text.Json.Serialization;
using AutoCita.Abstractions;
using AutoCita.Enums;

namespace AutoCita.Models
{
    internal class Usuario : Persona
    {
        public string NombreUsuario { get; set; }
        public string PasswordHash { get; set; }
        public bool Estado { get; set; }

        [JsonConstructor]
        public Usuario(Guid id, string nombre, string apellido, string telefono, string email, DateOnly fechaNacimiento, string numeroDocumento, 
            TipoDocumento tipoDocumento, string nombreUsuario, string passwordHash, bool estado) 
            : base(id, nombre, apellido, telefono, email, fechaNacimiento, numeroDocumento, tipoDocumento)
        {
            NombreUsuario = nombreUsuario;
            PasswordHash = passwordHash;
            Estado = estado;
        }

        public override bool ValidarInformacion()
        {
            if (
                string.IsNullOrEmpty(Nombre.Trim()) || string.IsNullOrEmpty(Apellido.Trim()) || string.IsNullOrEmpty(Telefono.Trim()) || 
                string.IsNullOrEmpty(Email.Trim()) || string.IsNullOrEmpty(NumeroDocumento.Trim()) || string.IsNullOrEmpty(NombreUsuario.Trim()) || 
                string.IsNullOrEmpty(PasswordHash.Trim())
            )
            {
                return false;
            }

            bool emailValido = Email.Contains('@');

            if (!emailValido)
            {
                return false;
            }

            return true;
        }

        public void ActualizarInformacion(string telefono, string email, string numeroDocumento, TipoDocumento tipoDocumento, string nombreUsuario, string passwordHash, bool estado)
        {
            bool informacionValida = ValidarInformacion();

            if (informacionValida)
            {
                Telefono = telefono;
                Email = email;
                NumeroDocumento = numeroDocumento;
                TipoDocumento = tipoDocumento;
                NombreUsuario = nombreUsuario;
                PasswordHash = passwordHash;
                Estado = estado;
            }
        }
    }
}
