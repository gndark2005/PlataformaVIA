namespace PlataformaVIA.Core.Domain.Seguridad
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Pregunta
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string RespuestaSeleccionada { get; set; }
        public List<Respuesta> Respuestas;
    }
}
