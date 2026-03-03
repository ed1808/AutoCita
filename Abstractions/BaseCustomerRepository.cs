using AutoCita.Interfaces;
using AutoCita.Models;

namespace AutoCita.Abstractions
{
    internal abstract class BaseCustomerRepository : ICustomerRepository
    {
        protected readonly string _dataSource;

        protected BaseCustomerRepository(string dataSource)
        {
            _dataSource = dataSource;
        }

        public async Task<bool> AddCustomer(Cliente customer)
        {
            if (!customer.ValidarInformacion())
            {
                return false;
            }

            return await PersistNewCustomer(customer);
        }

        public async Task<List<Cliente>> GetCustomers()
        {
            return await LoadAllCustomers();
        }

        public async Task<Cliente?> GetCustomer(Guid id)
        {
            return await LoadCustomerById(id);
        }

        public async Task<Cliente?> GetCustomerByDocument(string documentNumber)
        {
            return await LoadCustomerByDocument(documentNumber);
        }

        public async Task<bool> UpdateCustomer(Cliente customer)
        {
            if (!customer.ValidarInformacion())
            {
                return false;
            }

            return await PersistUpdatedCustomer(customer);
        }

        protected abstract Task<bool> PersistNewCustomer(Cliente customer);
        protected abstract Task<List<Cliente>> LoadAllCustomers();
        protected abstract Task<Cliente?> LoadCustomerById(Guid id);
        protected abstract Task<Cliente?> LoadCustomerByDocument(string documentNumber);
        protected abstract Task<bool> PersistUpdatedCustomer(Cliente customer);
    }
}
