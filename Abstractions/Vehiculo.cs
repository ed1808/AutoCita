using System.Text.Json.Serialization;
using AutoCita.Models;

namespace AutoCita.Abstractions
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "tipoVehiculo")]
    [JsonDerivedType(typeof(Automovil), "automovil")]
    [JsonDerivedType(typeof(Camion), "camion")]
    [JsonDerivedType(typeof(Camioneta), "camioneta")]
    [JsonDerivedType(typeof(Motocicleta), "motocicleta")]
    internal abstract class Vehiculo
    {
        public Guid Id { get; set; }
        public string Vin { get; set; }
        public string Placa { get; set; }
        public string Marca { get; set; }
        public string Linea { get; set; }
        public int Modelo { get; set; }
        public int Kilometraje { get; set; }
        public Guid PropietarioId { get; set; }

        protected Vehiculo(Guid id, string vin, string placa, string marca, string linea, int modelo, int kilometraje, Guid propietarioId)
        {
            Id = id;
            Vin = vin;
            Placa = placa;
            Marca = marca;
            Linea = linea;
            Modelo = modelo;
            Kilometraje = kilometraje;
            PropietarioId = propietarioId;
        }

        public abstract string GetInfo();

        public abstract bool ValidarInformacion();

        public virtual void ActualizarKilometraje(int nuevoKilometraje)
        {
            if (nuevoKilometraje > Kilometraje)
            {
                Kilometraje = nuevoKilometraje;
            }
        }
    }
}
