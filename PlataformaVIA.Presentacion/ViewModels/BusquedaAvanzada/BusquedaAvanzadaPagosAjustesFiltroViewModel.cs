namespace PlataformaVIA.Presentacion.ViewModels.BusquedaAvanzada
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class BusquedaAvanzadaPagosAjustesFiltroViewModel
    {
        [Display(Name = "Periodo")]
        [Required(ErrorMessage = "Por favor seleccionar {0}")]
        [Range(1000, 10000, ErrorMessage = "Por favor seleccionar {0}")]
        public int CodCicloFacturacion { get; set; }

        public SelectList CicloFacturacionList { get; set; }

        [Display(Name = "Busqueda por")]
        public int CodTipoFiltro { get; set; }
        public SelectList TipoFiltroList { get; set; }

        public string Valor { get; set; }
    }
}