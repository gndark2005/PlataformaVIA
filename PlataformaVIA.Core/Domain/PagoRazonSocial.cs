namespace PlataformaVIA.Core.Domain
{
    using System;
    using System.Collections.Generic;

    public class PagoRazonSocial
    {
        public string ReferenciaPago { get; set; }
        //public Banco BancoTransaccion { get; set; }
        public string Banco { get; set; }
        public decimal ValorPago { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaAplicacionPago { get; set; }
        //public IEnumerable<DistribucionPagoRazonSocial> Distribuciones { get; set; }
    }
}
