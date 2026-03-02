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

        public bool AddCustomer(Cliente customer)
        {
            return SaveCustomerToDataSource(customer);
        }

        public Cliente? GetCustomer(string id)
        {
            GetCustomerFromDataSource(id);

            return null;
        }

        public List<Cliente> GetCustomers()
        {
            GetCustomersFromDataSource();

            return [];
        }

        public bool UpdateCustomer(Cliente customer)
        {
            return UpdateCustomerInDataSource(customer);
        }

        protected abstract bool SaveCustomerToDataSource(Cliente customer);
        protected abstract void GetCustomersFromDataSource();
        protected abstract void GetCustomerFromDataSource(string id);
        protected abstract bool UpdateCustomerInDataSource(Cliente customer);
    }
}
