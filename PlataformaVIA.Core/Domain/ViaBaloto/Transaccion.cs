using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaVIA.Core.Domain.ViaBaloto
{
    public class Transaccion
    {
        public long IdTransaccion { set; get; }
        public string FechaHoraTransaccion { set; get; }
        public int CodigoPuntoDeVenta { set; get; }
        public string NombrePuntoDeVenta { set; get; }
        public decimal ValorTransaccion { set; get; }
        public string Referencia { set; get; } //nuevo
        public DateTime FechaReporteAliado { set; get; }//nuevo
        
    }
}
