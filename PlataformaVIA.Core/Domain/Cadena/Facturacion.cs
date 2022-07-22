namespace PlataformaVIA.Core.Domain.Cadena
{
    using System;

    public class Facturacion
    {
        public string Periodo { get; set; }
        public DateTime FechaLimite { get; set; }
        public decimal SaldoPrevio { get; set; }
        public decimal ValorIGT { get; set; }
        public decimal ValorFiducia { get; set; }
        public decimal Total { get; set; }
        public string RutaArchivo { get; set; }
    }
}