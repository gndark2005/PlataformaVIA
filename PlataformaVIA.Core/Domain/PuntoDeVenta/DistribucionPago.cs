using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PlataformaVIA.Core.Domain.PuntoDeVenta
{
    public class DistribucionPago
    {
        [Display(Name = "Punto de venta")]
        public string CodigoPuntoDeVenta { get; set; }
        [Display(Name = "Cod. Cadena")]
        public string CodigoCadena { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name = "Facturas y Depósitos")]
        public decimal FacturasDepositos { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name = "Pines y recargas")]
        public decimal PinesRecargas { get; set; }
        [DataType(DataType.Currency)]
        public decimal Total { get; set; }
    }
}
