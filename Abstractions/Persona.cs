using AutoCita.Enums;

namespace AutoCita.Abstractions
{
    internal abstract class Persona
    {
        public Guid Id { get; private set; }
        public string Nombre { get; private set; }
        public string Apellido { get; private set; }
        public string Telefono { get; protected set; }
        public string Email { get; protected set; }
        public DateOnly FechaNacimiento { get; private set; }
        public string NumeroDocumento { get; protected set; }
        public TipoDocumento TipoDocumento { get; protected set; }

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
