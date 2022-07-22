using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaVIA.Core.Domain.ViaBaloto
{
    public class PuntoDeVenta
    {
        public  string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string NombreCiudad { get; set; }
        public string Distancia { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
    }
}
