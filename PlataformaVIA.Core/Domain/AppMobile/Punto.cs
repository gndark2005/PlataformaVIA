namespace PlataformaVIA.Core.Domain.AppMobile
{
    using System;

    public class Punto
    {
        public Int64 Id_Punto { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public double Longitud { get; set; }
        public double Latitud { get; set; }
        public float DistanceKm { get; set; }
    }
}
