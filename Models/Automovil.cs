using AutoCita.Abstractions;

namespace AutoCita.Models
{
    internal class Automovil : Vehiculo
    {
        public int NumeroPuertas;
        public string TipoCombustible;
        public string Transmision;

        public Automovil(Guid id, string vin, string placa, string marca, string linea, int modelo, int kilometraje, Cliente propietario, 
            int numeroPuertas, string tipoCombustible, string transmision) 
            : base(id, vin, placa, marca, linea, modelo, kilometraje, propietario)
        {
            NumeroPuertas = numeroPuertas;
            TipoCombustible = tipoCombustible;
            Transmision = transmision;
        }

        public override string GetInfo()
        {
            return $"Automóvil: {Marca} {Linea} ({Modelo}), Placa: {Placa}, VIN: {Vin}, Puertas: {NumeroPuertas}, Combustible: {TipoCombustible}, " + 
                   $"Transmisión: {Transmision}, Kilometraje: {Kilometraje}";
        }

        public override bool ValidarInformacion()
        {
            if (
                string.IsNullOrWhiteSpace(Vin) || string.IsNullOrWhiteSpace(Placa) || string.IsNullOrWhiteSpace(Marca) || string.IsNullOrWhiteSpace(Linea) || 
                NumeroPuertas <= 0 || string.IsNullOrWhiteSpace(TipoCombustible) || string.IsNullOrWhiteSpace(Transmision)
            )
            {
                return false;
            }

            return true;
        }
    }
}
