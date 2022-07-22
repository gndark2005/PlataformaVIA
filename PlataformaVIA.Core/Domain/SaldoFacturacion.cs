using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaVIA.Core.Domain
{
    public class SaldoFacturacion
    {
        public ParticipacionLineaNegocioEnum ConceptoPago;
        public DateTime FechaLimitePago;
        public decimal SaldoTotalPagar;
    }
}
