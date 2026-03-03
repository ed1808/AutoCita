using System.Text.Json.Serialization;
using AutoCita.Abstractions;

namespace AutoCita.Models
{
    internal class Camioneta : Vehiculo
    {
        public bool EsDobleTraccion { get; set; }
        public double CapacidadCarga { get; set; }

        [JsonConstructor]
        public Camioneta(Guid id, string vin, string placa, string marca, string linea, int modelo, int kilometraje, Guid propietarioId,
            bool esDobleTraccion, double capacidadCarga)
            : base(id, vin, placa, marca, linea, modelo, kilometraje, propietarioId)
        {
            EsDobleTraccion = esDobleTraccion;
            CapacidadCarga = capacidadCarga;
        }

        public override string GetInfo()
        {
            return $"Camioneta - VIN: {Vin}, Placa: {Placa}, Marca: {Marca}, Línea: {Linea}, Modelo: {Modelo}, Kilometraje: {Kilometraje}, " + 
                   $"Doble Tracción: {(EsDobleTraccion ? "Sí" : "No")}, Capacidad de Carga: {CapacidadCarga} kg";
        }

        public override bool ValidarInformacion()
        {
            if (string.IsNullOrWhiteSpace(Vin) || string.IsNullOrWhiteSpace(Placa) || string.IsNullOrWhiteSpace(Marca) || string.IsNullOrWhiteSpace(Linea))
            {
                return false;
            }

            if (CapacidadCarga < 0)
            {
                return false;
            }

            return true;
        }
    }
}
