using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaVIA.Core.Domain
{
    public class ResultadoValidacionSeguridad
    {
        public string Nit { get; set; }
        public string TokenResultado { get; set; }
        public List<ValidacionRespuestaSeguridad> ValoresContestados { get; set; }
    }
}
