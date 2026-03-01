using AutoCita.Abstractions;

namespace AutoCita.Models
{
    internal class Motocicleta : Vehiculo
    {
        public string Tipo;

        public Motocicleta(string id, string vin, string placa, string marca, string linea, int modelo, int kilometraje, Cliente propietario, string tipo) : base(id, vin, placa, marca, linea, modelo, kilometraje, propietario)
        {
            Tipo = tipo;
        }

        public override string GetInfo()
        {
            return $"Motocicleta: {Marca} {Linea} ({Modelo}), Placa: {Placa}, VIN: {Vin}, Tipo: {Tipo}, Kilometraje: {Kilometraje}";
        }
    }
}
