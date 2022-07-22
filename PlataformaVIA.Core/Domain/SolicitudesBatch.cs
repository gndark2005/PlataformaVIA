using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaVIA.Core.Domain
{
    public class SolicitudesBatch
    {
        public Int64 ID_SOLICITUDENVIOREPORTE { get; set; }
        public int CODUSUARIOINFOSOLICITUD { get; set; }
        public TipoSolicitudEnvioReporteEnum CodTipoSolicitudEnvioReporte { get; set; }
        public int ?CodPuntoDeVenta { get; set; }
        public int ?CodCadena { get; set; }
        public int CodCicloFacturacion { get; set; }
        public string EMAILREPORTE { set; get; }
        public DateTime ?FECHAREFERENCIA { set; get; }
    }
}
