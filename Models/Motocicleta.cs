using System.Text.Json.Serialization;
using AutoCita.Abstractions;

namespace AutoCita.Models
{
    internal class Motocicleta : Vehiculo
    {
        public string Tipo { get; set; }

        [JsonConstructor]
        public Motocicleta(Guid id, string vin, string placa, string marca, string linea, int modelo, int kilometraje, Guid propietarioId, string tipo) 
            : base(id, vin, placa, marca, linea, modelo, kilometraje, propietarioId)
        {
            Tipo = tipo;
        }

        public override string GetInfo()
        {
            return $"Motocicleta: {Marca} {Linea} ({Modelo}), Placa: {Placa}, VIN: {Vin}, Tipo: {Tipo}, Kilometraje: {Kilometraje}";
        }

        public override bool ValidarInformacion()
        {
            if (
                string.IsNullOrWhiteSpace(Vin) || string.IsNullOrWhiteSpace(Placa) || string.IsNullOrWhiteSpace(Marca) || 
                string.IsNullOrWhiteSpace(Linea) ||  string.IsNullOrWhiteSpace(Tipo)
            )
            {
                return false;
            }

            return true;
        }
    }
}
