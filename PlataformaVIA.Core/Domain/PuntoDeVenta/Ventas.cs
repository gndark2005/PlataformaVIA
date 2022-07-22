namespace PlataformaVIA.Core.Domain.PuntoDeVenta
{
    using System;

    public class Ventas
    {
        //public string RangoFecha { get; set; }
        public DateTime Fecha { get; set; }
        /// <summary>
        /// Es la suma de las transacciones realizadas en la fecha seleccionada  para la línea de negocio
        /// </summary>
        public decimal Valor { get; set; }
        //public LineadeNegocioEnum LineaDeNegocio { get; set; }
        public string LineaDeNegocio { get; set; }
    }
}