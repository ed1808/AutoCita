using AutoCita.Models;

namespace AutoCita.Interfaces
{
    internal interface ICustomerRepository
    {
        bool AddCustomer(Cliente customer);
        List<Cliente> GetCustomers();
        Cliente? GetCustomer(string id);
        bool UpdateCustomer(Cliente customer);
    }
}
