using AutoCita.Models;

namespace AutoCita.Abstractions
{
    internal abstract class Vehiculo
    {
        protected string Id;
        protected string Vin;
        protected string Placa;
        protected string Marca;
        protected string Linea;
        protected int Modelo;
        protected int Kilometraje;
        protected Cliente Propietario;
        protected List<Cita> HistorialCitas;

        protected Vehiculo(string id, string vin, string placa, string marca, string linea, int modelo, int kilometraje, Cliente propietario)
        {
            Id = id;
            Vin = vin;
            Placa = placa;
            Marca = marca;
            Linea = linea;
            Modelo = modelo;
            Kilometraje = kilometraje;
            Propietario = propietario;
            HistorialCitas = [];
        }

        public abstract string GetInfo();

        public virtual void ActualizarKilometraje(int nuevoKilometraje)
        {
            if (nuevoKilometraje > Kilometraje)
            {
                Kilometraje = nuevoKilometraje;
            }
        }

        public virtual void AsignarPropietario(Cliente nuevoPropietario)
        {
            Propietario = nuevoPropietario;
        }
    }
}
