using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlataformaVIA.Presentacion.ViewModels.BusquedaAvanzada
{
    public class BusquedaAvanzadaFiltrosViewModel
    {
        [Display(Name = "Filtro de Busqueda")]
        public SelectList OpcionesBusquedaList { get; set; }
        public string tipoBusqueda { get; set; }
    }
}