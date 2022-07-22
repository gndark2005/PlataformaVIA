using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaVIA.Core.Domain
{
    public class Notificaciones
    {
        public Int64 ID_NOTIFICACION { get; set; }
        public Int64 ID_NOTIFICACIONUSUARIOINFO { get; set; }
        [Display(Name = "Asunto")]
        public string TITULO { get; set; }
        [Display(Name = "Mensaje")]
        public string MENSAJE { get; set; }
        public string URL { get; set; }
        [Display(Name = "Leída")]
        public bool LEIDA { get; set; }
        [Display(Name = "Fecha")]
        public DateTime FECHAHORAINGRESO { get; set; }
        public string TOKENNOTIFICADO { get; set; }
        public string TOKEN { get; set; }
        public string AGENTENOTIFICACION { get; set; }
        public TipoIngresoNotificacionEnum TipoIngresoNotificacion { get; set; }
        public TipoTokenNotificacionEnum TipoTokenNotificacion { get; set; }
    }
}
