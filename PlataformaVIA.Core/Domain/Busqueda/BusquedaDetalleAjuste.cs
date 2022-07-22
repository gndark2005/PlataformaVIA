using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaVIA.Core.Domain.Busqueda
{
    public class BusquedaDetalleAjuste
    {      
        [Display(Name = "Fecha y Hora")]
        public string FechaHora { get; set; }    
        [Display(Name = "Valor")]
        public string Valor { get; set; }
        [Display(Name = "Categoría")]
        public string Categoria { get; set; }
        [Display(Name = "Descripcion:")]
        public string Descripcion { get; set; }
    }
}
