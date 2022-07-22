using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaVIA.Core.Domain.AdministradorDocumentos
{
    public class ImagenesInstructivo
    {
        public int IdInstructivo { get; set; }
        public int IdInstructivoTipoTerminal { get; set; }
        public byte Orden { get; set; }
        public string Ubicacion { get; set; }
        public string NombreArchivo { get; set; } 
        public string Atributo { get; set; }
        public string UrlImagen { get; set; }
    }
}
