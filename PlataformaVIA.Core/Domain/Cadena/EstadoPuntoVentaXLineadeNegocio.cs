namespace PlataformaVIA.Core.Domain.Cadena
{
    public class EstadoPuntoVentaXLineadeNegocio
    {
        public int CodigoPuntoVenta { get; set; }
        public string Nombre { get; set; }
        //public EstadoPuntoVentaEnum EstadoJuego { get; set; }
        //public EstadoPuntoVentaEnum EstadoDeposito { get; set; }
        //public EstadoPuntoVentaEnum EstadoPinesRecarga { get; set; }
        public string EstadoJuego { get; set; }
        public string EstadoDeposito { get; set; }
        public string EstadoPinesRecarga { get; set; }
        public string Estado { get; set; }
    }
}
