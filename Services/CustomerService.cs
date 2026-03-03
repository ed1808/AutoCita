using AutoCita.Interfaces;
using AutoCita.Models;

namespace AutoCita.Services
{
    internal class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<bool> RegistrarCliente(Cliente customer)
        {
            if (!customer.ValidarInformacion())
            {
                return false;
            }

            var existente = await _customerRepository.GetCustomerByDocument(customer.NumeroDocumento);

            if (existente is not null)
            {
                return false;
            }

            return await _customerRepository.AddCustomer(customer);
        }

        public async Task<List<Cliente>> ObtenerClientes()
        {
            return await _customerRepository.GetCustomers();
        }

        public async Task<Cliente?> ObtenerClientePorId(Guid id)
        {
            return await _customerRepository.GetCustomer(id);
        }

        public async Task<Cliente?> ObtenerClientePorDocumento(string numeroDocumento)
        {
            return await _customerRepository.GetCustomerByDocument(numeroDocumento);
        }

        public async Task<bool> ActualizarCliente(Cliente customer)
        {
            if (!customer.ValidarInformacion())
            {
                return false;
            }

            var existente = await _customerRepository.GetCustomer(customer.Id);

            if (existente is null)
            {
                return false;
            }

            return await _customerRepository.UpdateCustomer(customer);
        }
    }
}
