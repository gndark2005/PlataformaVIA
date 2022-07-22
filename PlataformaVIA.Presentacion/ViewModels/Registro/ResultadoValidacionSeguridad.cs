namespace PlataformaVIA.Presentacion.ViewModels.Registro
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class ResultadoValidacionSeguridad
    {
        public string Nit { get; set; }
        public string TokenResultado { get; set; }
        public List<ValidacionRespuesta> ValoresContestados { get; set; }
    }
}