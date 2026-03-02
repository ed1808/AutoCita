using AutoCita.Abstractions;
using AutoCita.Enums;

namespace AutoCita.Models
{
    internal class Usuario : Persona
    {
        public string NombreUsuario;
        public string PasswordHash;
        public bool Estado;

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
                string.IsNullOrWhiteSpace(Nombre) || string.IsNullOrWhiteSpace(Apellido) || string.IsNullOrWhiteSpace(Telefono) || 
                string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(NumeroDocumento) || string.IsNullOrWhiteSpace(NombreUsuario) || 
                string.IsNullOrWhiteSpace(PasswordHash)
            )
            {
                return false;
            }

            bool passwordValida = VerificarPassword(PasswordHash);

            if (!passwordValida)
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

        private bool VerificarPassword(string password)
        {
            // Aquí se debería implementar la lógica para verificar el hash de la contraseña
            // Por simplicidad, vamos a comparar directamente el hash (esto no es seguro en un entorno real)
            return PasswordHash == password;
        }
    }
}
