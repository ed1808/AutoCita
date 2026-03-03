using AutoCita.Models;

namespace AutoCita.Interfaces
{
    internal interface ICustomerService
    {
        Task<bool> RegistrarCliente(Cliente customer);
        Task<List<Cliente>> ObtenerClientes();
        Task<Cliente?> ObtenerClientePorId(Guid id);
        Task<Cliente?> ObtenerClientePorDocumento(string numeroDocumento);
        Task<bool> ActualizarCliente(Cliente customer);
    }
}
