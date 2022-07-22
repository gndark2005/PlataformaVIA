namespace PlataformaVIA.Presentacion.ViewModels.BusquedaAvanzada
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    public class BusquedaAvanzadaRangoFechasViewModel
    {        
        [Display(Name = "Fecha Inicio")]
        [Required(ErrorMessage = "Por favor seleccionar {0}")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.Date)]
        public DateTime FechaInicio { get; set; }
        public int CodProducto { get; set; }
        [Display(Name = "Fecha Final")]
        [Required(ErrorMessage = "Por favor seleccionar {0}")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.Date)]
        public DateTime FechaFin { get; set; }
    }
}