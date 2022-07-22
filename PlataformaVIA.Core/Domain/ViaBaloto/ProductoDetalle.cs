using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaVIA.Core.Domain.ViaBaloto
{
    public class ProductoDetalle
    {
        public string Logo { get; set; }
        public string PaginaWeb { get; set; }
        public string ResenaProducto { get; set; }
        public string NumeroProducto { get; set; }
        public string NumeroConvenio { get; set; }
        public string NombreProducto { get; set; }
        public string NombreConvenio { get; set; }
        public string ReferenciaCaptura { get; set; }
        public int CantidadCaracteresReferenciaCaptura { get; set; }
        public decimal MontoMinimo { get; set; }
        public decimal MontoMaximo { get; set; }
        public Boolean AceptaVencidas { get; set; }
        public Boolean AceptaPagosDobles { get; set; }
        public string TipoVinculacion { get; set; }//nuevo
        public int CodigoAliadoEstrategico { get; set; }//nuevo
        public string RazonSocialAliado { get; set; }//nuevo
        public string DatosContacto { get; set; }//nuevo, mensaje frente al tiquete
        public string CanalDeAtencionUsuario { get; set; }//nuevo
        public Int64 NumeroProductoInterno { get; set; } //Nuevo
    }
}
