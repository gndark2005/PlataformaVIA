using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PlataformaVIA.Core.Domain.AdministradorDocumentos
{
    public class Instructivos
    {
        public int IdInstructivo { get; set; }

        [Display(Name = "Tipo de Documento:")]
        public string TipoInstructivo  { get; set; }

        [Display(Name = "Tipo de Documento:")]
        [Range(1, 999, ErrorMessage = "Debe seleccionar un tipo de documento")]
        [Required(ErrorMessage = "Debe seleccionar {0}")]
        public int CodTipoInstructivo { get; set; }

        [Display(Name = "Categoría:")]
        public string Categoria { get; set; }

        [Display(Name = "Categoría:")]
        //[Range(1, 999, ErrorMessage = "Debe seleccionar {0}")]
        [Required(ErrorMessage = "Debe seleccionar {0}")]
        public int CodCategoria { get; set; }

        [Display(Name = "Titulo del Documento:")]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "{0} No cumple con la longitud")]
        public string Titulo { get; set; }

        [Display(Name = "Descripcion:")]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "{0} No cumple con la longitud")]
        public string Descripcion   { get; set; }
        //?
        public string Ubicacion     { get; set; }

        public string Atributo { get; set; }

        [Display(Name = "Tipo de Terminal:")]       
        public string TipoTerminal { get; set; }

        public string NombreArchivo { get; set; }

        [Display(Name = "Tipo de Terminal:")]
        //[Range(1, 999, ErrorMessage = "Debe seleccionar {0}")]
        [Required(ErrorMessage = "Debe seleccionar {0}")]
        public int CodTipoTerminal     { get; set; }

        public IEnumerable<HttpPostedFileBase> Files { get; set; }

        public List<String> TipoImagen { get; set; }

        public string URL { get; set; }

        public bool MultiplesArchivos { get; set; }

    }
}
