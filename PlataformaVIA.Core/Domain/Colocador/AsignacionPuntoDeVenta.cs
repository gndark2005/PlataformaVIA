using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaVIA.Core.Domain.Colocador
{
    public class AsignacionPuntoDeVenta
    {
        public int CodPuntoDeVenta { get; set; }
        public int CodColocador { get; set; }
        public bool Asignado { get; set; }
    }
}
