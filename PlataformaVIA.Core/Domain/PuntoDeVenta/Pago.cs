namespace PlataformaVIA.Core.Domain.PuntoDeVenta
{
    using System;

    public class Pago
    {
        public int Id { get; set; }
        public string ReferenciaPago { get; set; }
        public string Banco { get; set; }
        public decimal ValorPago { get; set; }
        public DateTime FechaPago { get; set; }
        /// <summary>
        /// Fecha en que fue aplicado el pago en SFG cartera
        /// </summary>
        public DateTime FechaAplicacionPago { get; set; }
        public string Agente { get; set; }
        //public IEnumerable<DistribucionPago> Distribuciones { get; set; }

    }
}
