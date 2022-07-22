namespace PlataformaVIA.Core.Domain
{
    using System.ComponentModel.DataAnnotations;

    public class EstadoCuentaXLineaDeNegocio
    {
        [Display(Name = "Línea de Negocio")]
        public string LineaDeNegocio { get; set; }/*LineadeNegocioEnum*/
        public string Estado { get; set; } /*EstadoPuntoVentaEnum*/
        [Display(Name = "Cupo Disponible")]
        [DataType(DataType.Currency)]
        public decimal CupoDisponible { get; set; }
        [Display(Name = "Cupo Disponible")]
        public string CupoDisponibleTexto { get; set; }
        [Display(Name = "Cupo Total")]
        [DataType(DataType.Currency)]
        public decimal CupoTotal { get; set; }
        [Display(Name = "Cupo Disponible")]
        public string CupoTotalTexto { get; set; }

    }
}
