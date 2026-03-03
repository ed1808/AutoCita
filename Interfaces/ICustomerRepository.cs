using AutoCita.Models;

namespace AutoCita.Interfaces
{
    internal interface ICustomerRepository
    {
        Task<bool> AddCustomer(Cliente customer);
        Task<List<Cliente>> GetCustomers();
        Task<Cliente?> GetCustomer(Guid id);
        Task<Cliente?> GetCustomerByDocument(string documentNumber);
        Task<bool> UpdateCustomer(Cliente customer);
    }
}
