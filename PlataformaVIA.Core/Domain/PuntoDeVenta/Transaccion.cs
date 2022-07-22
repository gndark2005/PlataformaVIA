namespace PlataformaVIA.Core.Domain.PuntoDeVenta
{
    using System;

    public class Transaccion
    {
        public DateTime FechaHora { get; set; }
        public LineadeNegocioEnum LineaDeNegocio { get; set; }
        public string Producto { get; set; }
        public decimal Valor { get; set; }
        public string TipoProducto { get; set; }
        public string Estado { get; set; }
        public string LineaDeNegocioStr { get; set; }

    }
}
