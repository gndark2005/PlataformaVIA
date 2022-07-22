using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaVIA.Core.Domain.RepresentanteLegal
{
    public class SolicitudCertificado
    {
        public int IdCertificado { get; set; }
        public int CodPeriodoFecha { get; set; }//TODO Cómo vamos a manejar los Ids de cada uno de los meses
        public bool EnviarCorreoElectronico { get; set; }
    }
}
