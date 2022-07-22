namespace PlataformaVIA.Core.Domain.Cadena
{
    public class DetalleCupoPuntodeVenta
    {
        public string CodigoPuntoVenta { get; set; }
        public string NombrePuntoVenta { get; set; }
        public string Ciudad { get; set; }
        public decimal CupoAsignadoPines { get; set; }
        public decimal CupoAsignadoBP { get; set; }
        public decimal CupoAsignadoRecargas { get; set; }
        public decimal CupoConsumidoBP { get; set; }
        public decimal CupoConsumidoRecargas { get; set; }
        public decimal CupoConsumidoPines { get; set; }
        public decimal PorcentajeDisponiblePines { get; set; }
        public decimal PorcentajeDisponibleBP { get; set; }
        public decimal PorcentajeDisponibleRecargas { get; set; }
    }
}
