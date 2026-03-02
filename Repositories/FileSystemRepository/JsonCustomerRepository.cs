using AutoCita.Abstractions;
using AutoCita.Models;

namespace AutoCita.Repositories.FileSystemRepository
{
    internal class JsonCustomerRepository : BaseCustomerRepository
    {
        public JsonCustomerRepository() : base(Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "AutoCita",
            "Clientes",
            "index.json"
        ))
        {
        }

        protected override void GetCustomerFromDataSource(string id)
        {
            throw new NotImplementedException();
        }

        protected override void GetCustomersFromDataSource()
        {
            throw new NotImplementedException();
        }

        protected override bool SaveCustomerToDataSource(Cliente customer)
        {
            throw new NotImplementedException();
        }

        protected override bool UpdateCustomerInDataSource(Cliente customer)
        {
            throw new NotImplementedException();
        }
    }
}
