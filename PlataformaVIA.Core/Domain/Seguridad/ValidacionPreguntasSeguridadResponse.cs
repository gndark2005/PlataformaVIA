namespace PlataformaVIA.Core.Domain.Seguridad
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ValidacionPreguntasSeguridadResponse
    {
        public string Token { get; set; }
        public List<Pregunta> Preguntas { get; set; }
        public bool ValidacionExitosa { get; set; }
        public bool UsuarioBloqueado { get; set; }
        public string Mensaje { get; set; }
    }
}
