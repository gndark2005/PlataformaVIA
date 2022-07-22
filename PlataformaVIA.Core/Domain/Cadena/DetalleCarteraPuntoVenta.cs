namespace PlataformaVIA.Core.Domain.Cadena
{
    public class DetalleCarteraPuntoVenta
    {
        public string CodigoPuntoVenta { get; set; }
        public string NombrePuntoVenta { get; set; }
        public string Ciudad { get; set; }
        public int DiasMora { get; set; }
        public decimal SaldoJuegos { get; set; }
        public decimal SaldoPines { get; set; }
        public decimal SaldoBP { get; set; }
        public decimal SaldoRetiros { get; set; }
        public decimal SaldoInstalaciones { get; set; }
        public decimal Total { get; set; }
    }
}
