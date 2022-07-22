using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaVIA.Core.Domain.AdministradorDocumentos
{
    public class TiposDocumento
    {
        public int IdInstructivo { get; set; }
        public string NombreInstructivo { get; set; }
        public bool DiferenciaTerminal { get; set; }
    }
}
