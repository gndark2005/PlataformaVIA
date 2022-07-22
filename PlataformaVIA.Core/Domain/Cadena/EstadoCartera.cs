using System.ComponentModel.DataAnnotations;

namespace PlataformaVIA.Core.Domain.Cadena
{
    public class EstadoCartera
    {
        //public LineadeNegocioEnum LineadeNegocio { get; set; }
        public string LineadeNegocio { get; set; }
        public int DiasMora { get; set; }
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = false)]
        public decimal TotalDeuda { get; set; }
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = false)]
        public decimal SaldoCorriente { get; set; }
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = false)]
        public decimal SaldoVencido { get; set; }
    }
}
