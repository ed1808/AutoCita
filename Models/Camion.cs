using AutoCita.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoCita.Models
{
    internal class Camion : Vehiculo
    {
        public int NumeroEjes;
        public string TipoCarga;
        public double CapacidadCarga;

        public Camion(string id, string vin, string placa, string marca, string linea, int modelo, int kilometraje, Cliente propietario,
            int numeroEjes, string tipoCarga, double capacidadCarga)
            : base(id, vin, placa, marca, linea, modelo, kilometraje, propietario)
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
    }
}
