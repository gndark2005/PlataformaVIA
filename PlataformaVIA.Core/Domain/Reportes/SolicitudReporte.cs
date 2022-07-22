using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaVIA.Core.Domain.Reportes
{
    public class SolicitudReporte
    {
        public int codUsuario { get; set; }
        public byte codSolicitud { get; set; }
        public int? codPDV { get; set; }
        public int? codCadena { get; set; }
        public int? codCiclo { get; set; }
        public DateTime? fechaReferencia { get; set; }

    }
}
