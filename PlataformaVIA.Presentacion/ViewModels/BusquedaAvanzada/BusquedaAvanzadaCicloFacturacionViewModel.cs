using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlataformaVIA.Presentacion.ViewModels.BusquedaAvanzada
{
    public class BusquedaAvanzadaCicloFacturacionViewModel
    {
        [Display(Name = "Periodo")]
        [Required(ErrorMessage = "Por favor seleccionar {0}")]
        public int CodCicloFacturacion { get; set; }

        [Display(Name = "Fecha Fecha Referencia")]
        [Required(ErrorMessage = "Por favor seleccionar {0}")]
        public DateTime FechaReferencia { get; set; }


        public string TipoConsulta { get; set; }
        public SelectList CicloFacturacionList { get; set; }
        public SelectList CicloPreFacturacionList { get; set; }
        public int CodPuntoVenta { get; set; }
        public int CodCadena { get; set; }

        [Display(Name = "Filtro")]
        //[Required(ErrorMessage = "Por favor seleccionar {0}")]
        public int CodTipoFiltro { get; set; }
        public string Valor { get; set; }
    }
}