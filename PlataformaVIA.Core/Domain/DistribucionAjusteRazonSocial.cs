using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaVIA.Core.Domain
{
    public class DistribucionAjusteRazonSocial
    {
        public string CodigoPuntoVenta { get; set; }
        public string CodigoCadena { get; set; }
        public decimal ValorFacturasDepositos { get; set; }
        public decimal ValorPinesRecargas { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
