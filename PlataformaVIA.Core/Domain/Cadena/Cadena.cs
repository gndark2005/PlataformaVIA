
namespace PlataformaVIA.Core.Domain.Cadena
{
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Cadena
    {
        public int Id { get; set; }
        public string CodigoCadena { get; set; }

        public int CodCadenaDetail { get; set; }

        [Display(Name = "Nombre cadena")]
        public string Nombre { get; set; }

        [Display(Name = "Tipo de cadena")]
        public string TipoCadena { get; set; } /*TipoCadenaEnum*/

        [Display(Name = "Código cabeza cad")]
        public int CodigoCabezaCadena { get; set; }

        [Display(Name = "Referencia de pago")]
        public string ReferenciaPago { get; set; }

        [Display(Name = "Cantidad de PDV")]
        public int NumeroPuntosVenta { get; set; }
        public decimal SaldoTotal { get; set; }
        public int DiasMora { get; set; }

        public string Nit { get; set; }
        public string RazonSocial { get; set; }

        public IEnumerable<EstadoCartera> EstadosCarteraxLDN { get; set; }
        public IEnumerable<EstadoPuntoVentaXLineadeNegocio> EstadosPuntosDeVentasAsociados { get; set; }
    }
}
