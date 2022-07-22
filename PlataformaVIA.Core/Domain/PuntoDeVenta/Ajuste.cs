namespace PlataformaVIA.Core.Domain.PuntoDeVenta
{
    using System;

    public class Ajuste
    {
        public int Id { get; set; }
        /// <summary>
        /// Fecha, hora de aplicación del ajuste.
        /// </summary>
        public DateTime FechaHora { get; set; }
        public decimal Valor { get; set; }
        public string Descripcion { get; set; }
        public string DescripcionTipo { get; set; }
        public TipoAjusteEnum Tipo { get; set; }
        //public IEnumerable<DistribucionAjuste> Distribuciones { get; set; }

    }
}
