using AutoCita.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoCita.Models
{
    internal class Camioneta : Vehiculo
    {
        public bool EsDobleTraccion;
        public double CapacidadCarga;

        public Camioneta(string id, string vin, string placa, string marca, string linea, int modelo, int kilometraje, Cliente propietario, bool esDobleTraccion, double capacidadCarga)
            : base(id, vin, placa, marca, linea, modelo, kilometraje, propietario)
        {
            EsDobleTraccion = esDobleTraccion;
            CapacidadCarga = capacidadCarga;
        }

        public override string GetInfo()
        {
            return $"Camioneta - VIN: {Vin}, Placa: {Placa}, Marca: {Marca}, Línea: {Linea}, Modelo: {Modelo}, Kilometraje: {Kilometraje}, Doble Tracción: {(EsDobleTraccion ? "Sí" : "No")}, Capacidad de Carga: {CapacidadCarga} kg";
        }
    }
}
