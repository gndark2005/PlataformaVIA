using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaVIA.Core.Domain.AppMobile
{
    public class InstructivoTerminal
    {
        public int Id_Instructivo { get; set; }
        public int CodTipoTerminal { get; set; }
        public int CodCategoria { get; set; }
        public string TituloPregunta { get; set; }
    }
}
