using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlataformaVIA.Presentacion.ViewModels.Registro
{
    public class ValidacionRespuesta
    {
        public int CodPregunta { get; set; }
        public int CodRespuestaSeleccionada { get; set; }
        public string RespuestaSeleccionada { get; set; }
    }
}