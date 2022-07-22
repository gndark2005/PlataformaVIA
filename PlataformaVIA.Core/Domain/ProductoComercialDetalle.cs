using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaVIA.Core.Domain
{
    public class ProductoComercialDetalle
    {
        public string CodigoEnTerminal { get; set; }
        public string DescripcionGeneral { get; set; }
        public string AliadoOBanco { get; set; }
        public string ReferenciaParaRecaudo { get; set; }
        public string MontosPermitidos { get; set; }
        public string TiempoAplicacionPago { get; set; }
        public string AceptaParciales { get; set; }
        public string AceptaVencidas { get; set; }
        public string AceptaPagosDobles { get; set; }
        public string AceptaAnulaciones { get; set; }
        public string RutaImagen { get; set; }

    }
}
