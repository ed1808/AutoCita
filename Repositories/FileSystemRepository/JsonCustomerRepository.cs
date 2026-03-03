using AutoCita.Abstractions;
using AutoCita.FileHandler;
using AutoCita.Models;

namespace AutoCita.Repositories.FileSystemRepository
{
    internal class JsonCustomerRepository : BaseCustomerRepository
    {
        public JsonCustomerRepository() : base(Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "AutoCita",
            "Data",
            "clientes.json"
        ))
        {
        }

        protected override async Task<List<Cliente>> LoadAllCustomers()
        {
            return await FileHandler<List<Cliente>>.GetData(_dataSource) ?? [];
        }

        protected override async Task<Cliente?> LoadCustomerById(Guid id)
        {
            var customers = await LoadAllCustomers();
            return customers.FirstOrDefault(c => c.Id == id);
        }

        protected override async Task<Cliente?> LoadCustomerByDocument(string documentNumber)
        {
            var customers = await LoadAllCustomers();
            return customers.FirstOrDefault(c => c.NumeroDocumento == documentNumber);
        }

        protected override async Task<bool> PersistNewCustomer(Cliente customer)
        {
            var customers = await LoadAllCustomers();
            customers.Add(customer);
            return await FileHandler<List<Cliente>>.SaveData(_dataSource, customers);
        }

        protected override async Task<bool> PersistUpdatedCustomer(Cliente customer)
        {
            var customers = await LoadAllCustomers();
            var index = customers.FindIndex(c => c.Id == customer.Id);

            if (index < 0)
            {
                return false;
            }

            customers[index] = customer;
            return await FileHandler<List<Cliente>>.SaveData(_dataSource, customers);
        }
    }
}
