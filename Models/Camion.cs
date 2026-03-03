using System.Text.Json.Serialization;
using AutoCita.Abstractions;

namespace AutoCita.Models
{
    internal class Camion : Vehiculo
    {
        public int NumeroEjes { get; set; }
        public string TipoCarga { get; set; }
        public double CapacidadCarga { get; set; }

        [JsonConstructor]
        public Camion(Guid id, string vin, string placa, string marca, string linea, int modelo, int kilometraje, Guid propietarioId,
            int numeroEjes, string tipoCarga, double capacidadCarga)
            : base(id, vin, placa, marca, linea, modelo, kilometraje, propietarioId)
        {
            NumeroEjes = numeroEjes;
            TipoCarga = tipoCarga;
            CapacidadCarga = capacidadCarga;
        }

        public override string GetInfo()
        {
            return $"Camión - VIN: {Vin}, Placa: {Placa}, Marca: {Marca}, Línea: {Linea}, Modelo: {Modelo}, " +
                   $"Kilometraje: {Kilometraje} km, Número de Ejes: {NumeroEjes}, " +
                   $"Tipo de Carga: {TipoCarga}, Capacidad de Carga: {CapacidadCarga} toneladas";
        }

        public override bool ValidarInformacion()
        {
            if (string.IsNullOrWhiteSpace(Vin) || string.IsNullOrWhiteSpace(Placa) || string.IsNullOrWhiteSpace(Marca) || string.IsNullOrWhiteSpace(Linea))
            {
                return false;
            }

            if (NumeroEjes <= 0 || CapacidadCarga < 0)
            {
                return false;
            }

            return true;
        }
    }
}
