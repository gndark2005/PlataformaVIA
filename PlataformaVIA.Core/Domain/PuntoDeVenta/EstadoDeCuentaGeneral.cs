namespace PlataformaVIA.Core.Domain.PuntoDeVenta
{
    using System;

    public class EstadoDeCuentaGeneral
    {
        public string LineaDeNegocio { get; set; }
        public decimal PagoTotalALaFecha { get; set; }
        public decimal PagoMiniMoIGT { get; set; }
        public decimal PagoMinimoFiducia { get; set; }
        public DateTime FechaLimitePago { get; set; }
        public decimal CupoDisponible { get; set; }
        public string CupoDisponibleTexto { get; set; }
        public string Estado { get; set; }
        public string MotivoEstado { get; set; }
    }
}
