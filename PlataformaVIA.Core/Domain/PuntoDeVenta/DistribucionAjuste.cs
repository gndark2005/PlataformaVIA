namespace PlataformaVIA.Core.Domain.PuntoDeVenta
{
    using System.ComponentModel.DataAnnotations;

    public class DistribucionAjuste
    {
        //public LineadeNegocioEnum LineaDeNegocio { get; set; }
        //public decimal Valor { get; set; }
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
