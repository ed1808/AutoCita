using AutoCita.Enums;
using AutoCita.Interfaces;
using AutoCita.Models;

namespace AutoCita.Services
{
    internal class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IVehicleRepository _vehicleRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository, ICustomerRepository customerRepository, IVehicleRepository vehicleRepository)
        {
            _appointmentRepository = appointmentRepository;
            _customerRepository = customerRepository;
            _vehicleRepository = vehicleRepository;
        }

        public async Task<bool> AgendarCita(Cita appointment)
        {
            // Verificar que el cliente existe
            var cliente = await _customerRepository.GetCustomer(appointment.ClienteId);

            if (cliente is null)
            {
                return false;
            }

            // Verificar que el vehículo existe
            var vehiculo = await _vehicleRepository.GetVehicle(appointment.VehiculoId);

            if (vehiculo is null)
            {
                return false;
            }

            // Verificar que el vehículo pertenece al cliente
            if (vehiculo.PropietarioId != appointment.ClienteId)
            {
                return false;
            }

            // Verificar que no haya conflicto de horarios para el mismo vehículo
            var citasVehiculo = await _appointmentRepository.GetAppointmentsByVehicle(appointment.VehiculoId);
            bool hayConflicto = citasVehiculo.Any(c =>
                c.Estado == EstadoCita.PROGRAMADA &&
                c.FechaHoraInicio < appointment.FechaHoraFin &&
                c.FechaHoraFin > appointment.FechaHoraInicio
            );

            if (hayConflicto)
            {
                return false;
            }

            return await _appointmentRepository.AddAppointment(appointment);
        }

        public async Task<List<Cita>> ObtenerCitas()
        {
            return await _appointmentRepository.GetAppointments();
        }

        public async Task<Cita?> ObtenerCitaPorId(Guid id)
        {
            return await _appointmentRepository.GetAppointment(id);
        }

        public async Task<List<Cita>> ObtenerCitasPorCliente(Guid clienteId)
        {
            return await _appointmentRepository.GetAppointmentsByCustomer(clienteId);
        }

        public async Task<List<Cita>> ObtenerCitasPorVehiculo(Guid vehiculoId)
        {
            return await _appointmentRepository.GetAppointmentsByVehicle(vehiculoId);
        }

        public async Task<bool> ActualizarCita(Cita appointment)
        {
            var existente = await _appointmentRepository.GetAppointment(appointment.Id);

            if (existente is null)
            {
                return false;
            }

            return await _appointmentRepository.UpdateAppointment(appointment);
        }

        public async Task<bool> CancelarCita(Guid id, string motivoCancelacion)
        {
            var cita = await _appointmentRepository.GetAppointment(id);

            if (cita is null)
            {
                return false;
            }

            if (cita.Estado == EstadoCita.CANCELADA || cita.Estado == EstadoCita.FINALIZADA)
            {
                return false;
            }

            cita.Cancelar(motivoCancelacion);
            return await _appointmentRepository.UpdateAppointment(cita);
        }

        public async Task<bool> ReprogramarCita(Guid id, DateTime nuevaFechaHoraInicio)
        {
            var cita = await _appointmentRepository.GetAppointment(id);

            if (cita is null)
            {
                return false;
            }

            if (cita.Estado != EstadoCita.PROGRAMADA)
            {
                return false;
            }

            cita.Reprogramar(nuevaFechaHoraInicio);

            // Verificar que no haya conflicto de horarios con la nueva fecha
            var citasVehiculo = await _appointmentRepository.GetAppointmentsByVehicle(cita.VehiculoId);
            bool hayConflicto = citasVehiculo.Any(c =>
                c.Id != cita.Id &&
                c.Estado == EstadoCita.PROGRAMADA &&
                c.FechaHoraInicio < cita.FechaHoraFin &&
                c.FechaHoraFin > cita.FechaHoraInicio
            );

            if (hayConflicto)
            {
                return false;
            }

            return await _appointmentRepository.UpdateAppointment(cita);
        }

        public async Task<bool> ActualizarEstadoCita(Guid id, EstadoCita nuevoEstado)
        {
            var cita = await _appointmentRepository.GetAppointment(id);

            if (cita is null)
            {
                return false;
            }

            // Validar transiciones de estado permitidas
            bool transicionValida = (cita.Estado, nuevoEstado) switch
            {
                (EstadoCita.PROGRAMADA, EstadoCita.EN_PROCESO) => true,
                (EstadoCita.PROGRAMADA, EstadoCita.NO_ASISTIO) => true,
                (EstadoCita.PROGRAMADA, EstadoCita.CANCELADA) => true,
                (EstadoCita.EN_PROCESO, EstadoCita.FINALIZADA) => true,
                (EstadoCita.EN_PROCESO, EstadoCita.CANCELADA) => true,
                _ => false
            };

            if (!transicionValida)
            {
                return false;
            }

            cita.ActualizarEstado(nuevoEstado);
            return await _appointmentRepository.UpdateAppointment(cita);
        }
    }
}
