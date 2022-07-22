using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaVIA.Core.Domain.PuntoDeVenta
{
    public class DistribucionDetalleAjuste
    {
        [Display(Name = "Punto de venta")]
        public int CODIGOPUNTODEVENTA { get; set; }
        [Display(Name = "Cod. Cadena")]
        public int CODIGOCADENA { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name = "Juegos IGT")]
        public decimal JuegosIGT { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name = "Juegos Fiducia")]
        public decimal JuegosFiducia { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name = "Pines Recargas")]
        public decimal PinesRecargas { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name = "Facturas y depositos")]
        public decimal FacturasDepositos { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name = "Retiros")]
        public decimal Retiros { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name = "Instalaciones")]
        public decimal Instalaciones { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name = "Total")]
        public decimal Total { get; set; }
        [Display(Name = "Fecha y Hora")]
        public string FechaHora { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name = "Valor")]
        public string Valor { get; set; }
        [Display(Name = "Categoría")]
        public string Categoria { get; set; }
        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }
    }
}
