namespace PlataformaVIA.Core.Domain.Seguridad
{
    using System.Collections.Generic;

    public class ValidacionPreguntasSeguridad
    {
        public string Nit { get; set; }
        public IEnumerable<Pregunta> PreguntasSeguridad { get; set; }
        public string Token { get; set; }
    }
}
