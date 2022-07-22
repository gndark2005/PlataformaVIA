using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaVIA.Core.Domain
{
    public class AdministracionCorreo
    {
        public long ID_CORREO { get; set; }
        public string TITULO { get; set; }
        public string ASUNTO { get; set; }
        public string MENSAJE { get; set; }
        public string DESTINATARIOS { get; set; }
        public string COPIA_DESTINATARIOS { get; set; }
        public string PATH_ADJUNTO { get; set; }
        public string PATH_STORAGE { get; set; }
        public byte REQUIERE_TOKEN { get; set; }
        public string TOKEN { get; set; }
        public long ID_ESTADO { get; set; }
        public string RESPUESTA { get; set; }
        public string ARCHIVO { get; set; }
    }
}
