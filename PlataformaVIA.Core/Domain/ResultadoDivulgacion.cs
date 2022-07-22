using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaVIA.Core.Domain
{
    public class ResultadoDivulgacion
    {
        public string CODASPNETUSER { get; set; }
        public string NOMBREUSUARIO { get; set; }
        public long NIT { get; set; }
        public DateTime FECHAHORA { get; set; }
        public bool ACEPTADO { get; set; }
        public string DIRECCIONIP { get; set; }
        public string BROWSERID { get; set; }
        public string COMPUTERID { get; set; }
        public string UBICACION { get; set; }
    }
}
