using AutoCita.Interfaces;
using AutoCita.Repositories.FileSystemRepository;
using AutoCita.Services;

namespace AutoCita
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Repositories
            var customerRepository = new JsonCustomerRepository();
            var vehicleRepository = new JsonVehicleRepository();
            var appointmentRepository = new JsonAppointmentRepository();

            // Services
            ICustomerService customerService = new CustomerService(customerRepository);
            IVehicleService vehicleService = new VehicleService(vehicleRepository, customerRepository);
            IAppointmentService appointmentService = new AppointmentService(appointmentRepository, customerRepository, vehicleRepository);

            Application.Run(new Form1(customerService, vehicleService, appointmentService));
        }
    }
}