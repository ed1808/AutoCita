using AutoCita.Interfaces;
using AutoCita.Models;

namespace AutoCita.Abstractions
{
    internal abstract class BaseAppointmentRepository : IAppointmentRepository
    {
        protected readonly string _dataSource;

        protected BaseAppointmentRepository(string dataSource)
        {
            _dataSource = dataSource;
        }

        public bool AddAppointment(Cita appointment)
        {
            return SaveAppointmentToDataSource(appointment);
        }

        public Cita? GetAppointment(string id)
        {
            GetAppointmentFromDataSource(id);

            return null;
        }

        public List<Cita> GetAppointments()
        {
            GetAppointmentsFromDataSource();

            return [];
        }

        public bool UpdateAppointment(Cita appointment)
        {
            return UpdateAppointmentFromDataSource(appointment);
        }

        protected abstract bool SaveAppointmentToDataSource(Cita appointment);
        protected abstract void GetAppointmentsFromDataSource();
        protected abstract void GetAppointmentFromDataSource(string id);
        protected abstract bool UpdateAppointmentFromDataSource(Cita appointment);
    }
}
