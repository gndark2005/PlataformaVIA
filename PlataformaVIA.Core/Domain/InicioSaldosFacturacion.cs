using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaVIA.Core.Domain
{
    public class InicioSaldosFacturacion
    {
        public DateTime FechaIGT { get; set; }
        public decimal SaldoIGT { get; set; }
        public DateTime FechaFiducia { get; set; }
        public decimal SaldoFiducia { get; set; }
    }
}
