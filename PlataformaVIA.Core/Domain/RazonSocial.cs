using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaVIA.Core.Domain
{
    public class RazonSocial
    {
        public int IdRazonSocial { get; set; }
        public string Nit { get; set; }
        public string DigitoVerificacion { get; set; }
        public string RegimenTributario { get; set; }
        public DateTime FechaMatriculaMercantil { get; set; }
        public string NumeroMatriculaMercantil { get; set; }
        public string ActividadEconomica { get; set; }
        public string NombreRepresentanteLegal { get; set; }
        public string IdentificacionRepresentanteLegal { get; set; }
        public string Direccion { get; set; }
        public string CodCiudad { get; set; }
        public string Telefono { get; set; }
        public string CorreoRepresentanteLegal { get; set; }
        public string CelularRepresentanteLegal { get; set; }
        public int CodUsuarioPrincipal { get; set; }

    }
}
