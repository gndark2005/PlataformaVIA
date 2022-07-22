using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaVIA.Core.Domain
{
    public class Divulgacion
    {
        public Int64 ID_DIVULGACION { get; set; }
        [DisplayName("Nombre Divulgación:")]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        public string NOMBRE { get; set; }
        [DisplayName("Título Divulgación:")]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        public string TITULO { get; set; }
        [DisplayName("Texto Divulgación:")]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        [AllowHtml]
        public string TEXTO { get; set; }
        [DisplayName("Fecha Inicial:")]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        public DateTime FECHAINICIO { get; set; }
        [DisplayName("Fecha Final:")]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        public DateTime FECHAFIN { get; set; }
        [DisplayName("Permite Rechazar:")]
        public bool OPCIONRECHAZAR { get; set; }
        [DisplayName("Aceptar Política de Tratamiento de Datos:")]
        public bool POLITICADETRATAMIENTO { get; set; }
        public bool HABILITADA { get; set; }
        public Int64 CODUSUARIOINFOMODIFICACION { get; set; }
        public DateTime FECHAHORAMODIFICACION { get; set; }
        [DisplayName("Dirigido a:")]
        [Required(ErrorMessage = "Debe seleccionar al menos un rol")]
        public string[] ROLES { get; set; }
        [DisplayName("Excepciones por NIT:")]        
        public string[] EXCEPCIONESNIT { get; set; }
        public bool ACEPTADO { get; set; }



        //DATOS ADICIONALES PARA LOS REPORTES DE ACEPTACION

        public string DIRECCIONIP { get; set; }
        public string BROWSERID { get; set; }
        public string COMPUTERID { get; set; }
        public string UBICACION { get; set; }

        public string RAZONSOCIAL { get; set; }
        public string NIT { get; set; }

        public string CONTAINERPATH { get; set;}
    }

    internal class AllowHtmlAttribute : Attribute
    {
    }
}
