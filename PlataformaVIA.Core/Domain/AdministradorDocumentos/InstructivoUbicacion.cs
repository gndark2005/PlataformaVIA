using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaVIA.Core.Domain.AdministradorDocumentos
{
    public class InstructivoUbicacion
    {
        public int IdInstructivoUbicacion { get; set; }
        public int CodInstructivoTerminal { get; set; }
        public string NombreArchivo { get; set; }
        public byte Orden { get; set; }
        public string Ubicacion { get; set; }
        public string TipoImagen { get; set; }
        public string URL { get; set; }
    }
}
