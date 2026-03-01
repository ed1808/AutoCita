using AutoCita.Abstractions;
using AutoCita.Enums;

namespace AutoCita.Models
{
    internal class Usuario : Persona
    {
        public string NombreUsuario;
        public string PasswordHash;
        public bool Estado;

        public Usuario(string id, string nombre, string apellido, string telefono, string email, DateOnly fechaNacimiento, string numeroDocumento, TipoDocumento tipoDocumento, string nombreUsuario, string passwordHash, bool estado) : base(id, nombre, apellido, telefono, email, fechaNacimiento, numeroDocumento, tipoDocumento)
        {
            NombreUsuario = nombreUsuario;
            PasswordHash = passwordHash;
            Estado = estado;
        }

        public bool CrearUsuario()
        {
            // Lógica para crear un nuevo usuario en la base de datos
            return true;
        }

        public bool ActualizarUsuario(string? telefono, string? email, string? passwordHash)
        {
            // Lógica para actualizar la información del usuario en la base de datos
            return true;
        }

        public bool Login(string password)
        {
            // Lógica para verificar el password ingresado con el PasswordHash almacenado
            return true;
        }

        public void Logout()
        {
            // Lógica para cerrar la sesión del usuario
        }
    }
}
