using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlataformaVIA.Core.Domain.Seguridad;

namespace PlataformaVIA.Core.Domain
{
    public class Registro
    {
        public string Ip { get; set; }
        public UsuarioInfo Usuario { get; set; }
        public string Navegador { get; set; }
        public Int64 IdError { get; set; }
        public string Mensaje { get; set; }
        public string inneException { get; set; }
        public DateTime FechaEvento { get; set; }
        public string Path { get; set; }
        public string Parametros { get; set; }
        public bool DispositivoMovil { get; set; }
        public TipoRegistroEvento TipoRegistro { get; set; } //Enum
    }
}
