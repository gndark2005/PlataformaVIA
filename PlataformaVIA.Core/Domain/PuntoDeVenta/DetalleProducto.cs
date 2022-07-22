using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaVIA.Core.Domain.PuntoDeVenta
{
    public class DetalleProducto
    {
        public double IdProducto { get; set; }

        public double CodSubProducto { get; set; }

        [Display(Name = "Nombre: ")]
        public string NombreProducto { get; set; }

        public int CategoriaInstructivo { get; set; }

        [Display(Name = "Descripción: ")]
        public string Descripcion { get; set; }

        [Display(Name = "Categoría: ")]
        public string Categoría { get; set; }

        [Display(Name = "Código Convenio: ")]
        public string CodigoConvenio { get; set; }

        [Display(Name = "Empresa: ")]
        public string Empresa { get; set; }

        [Display(Name = "Convenio: ")]
        public string Convenio { get; set; }

        [Display(Name = "Código: ")]
        public string CodigoTerminal { get; set; }

        [Display(Name = "Aliado o banco: ")]
        public string AliadoEstrategico { get; set; }

        [Display(Name = "Referencia de recaudo: ")]
        public string ReferenciaCaptura { get; set; }

        [Display(Name = "Acepta facturas vencidas: ")]
        public bool AceptaVencidos { get; set; }

        [Display(Name = "Acepta pagos parciales: ")]
        public bool AceptaParciales { get; set; }

        [Display(Name = "Montos permitidos: ")]
        public string MontosPermitidos { get; set; }

        [Display(Name = "Cobertura: ")]
        public string CoberturaProducto { get; set; }

        [Display(Name = "Pagina Web: ")]
        public string PaginaWeb { get; set; }

        public int EsSubproducto { get; set; }

    }
}
