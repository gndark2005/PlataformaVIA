namespace PlataformaVIA.Core.Domain.AppMobile
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ProductoComercialDetalle
    {
        public Int64 Id { get; set; }
        public string NombreProducto { get; set; }
        public string Cliente { get; set; }
        public string CodigoTerminal { get; set; }
        public string CodigoProducto { get; set; }
        public string DescripcionProducto { get; set; }
        public string Empresa { get; set; }
        public string Recomendacion { get; set; }
        public int CodCategoria { get; set; }
        public string Categoria { get; set; }
        public string TiempoAplicacionPago { get; set; }
        public string Cobertura { get; set; }
        public string ComoRealizarTransaccion { get; set; }
        public string ReferenciaCaptura { get; set; }
        public string PermiteAnulaciones { get; set; }
        public string AceptaVencidos { get; set; }
        public string AceptaParciales { get; set; }
        public string AceptaPagosDobles { get; set; }
        public string MontoMinimo { get; set; }
        public string MontoMaximo { get; set; }
        public string NombreSubproducto { get; set; }
        public string CodigoConvenio { get; set; }
        public string Convenio { get; set; }
        public bool Estado { get; set; }
    }
}
