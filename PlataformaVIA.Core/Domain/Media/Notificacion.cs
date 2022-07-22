using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaVIA.Core.Domain.Media
{
    public class Notificacion
    {
        public string Asunto { get; set; }
        public string Detalle { get; set; }
        public DateTime Fecha { get; set; }
    }
}
