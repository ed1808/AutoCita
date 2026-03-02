using AutoCita.Abstractions;

namespace AutoCita.Models
{
    internal class Camioneta : Vehiculo
    {
        public bool EsDobleTraccion;
        public double CapacidadCarga;

        public Camioneta(Guid id, string vin, string placa, string marca, string linea, int modelo, int kilometraje, Cliente propietario,
            bool esDobleTraccion, double capacidadCarga)
            : base(id, vin, placa, marca, linea, modelo, kilometraje, propietario)
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
