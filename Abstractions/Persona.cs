using AutoCita.Enums;

namespace AutoCita.Abstractions
{
    internal abstract class Persona
    {
        protected Guid Id;
        protected string Nombre;
        protected string Apellido;
        protected string Telefono;
        protected string Email;
        protected DateOnly FechaNacimiento;
        protected string NumeroDocumento;
        protected TipoDocumento TipoDocumento;

        protected Persona(Guid id, string nombre, string apellido, string telefono, string email, DateOnly fechaNacimiento, string numeroDocumento, TipoDocumento tipoDocumento)
        {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            Telefono = telefono;
            Email = email;
            FechaNacimiento = fechaNacimiento;
            NumeroDocumento = numeroDocumento;
            TipoDocumento = tipoDocumento;
        }

        public virtual string GetNombreCompleto()
        {
            return $"{Nombre} {Apellido}";
        }

        public virtual string GetInfoContacto()
        {
            return $"Teléfono: {Telefono}, Email: {Email}";
        }

        public virtual int GetEdad()
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);
            int age = today.Year - FechaNacimiento.Year;

            if (FechaNacimiento > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }

        public abstract bool ValidarInformacion();
    }
}
