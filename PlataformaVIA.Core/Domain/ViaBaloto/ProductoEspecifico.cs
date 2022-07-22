using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PlataformaVIA.Core.Domain.ViaBaloto
{
    public class ProductoEspecifico
    {
        public string Logo { get; set; }
        public string NombreProducto { get; set; }
        public string ResenaProducto { get; set; }
        public decimal MontoMinimo { get; set; }
        public decimal MontoMaximo { get; set; }
        public Boolean AceptaVencidas { get; set; }
        public string NumeroProducto { get; set; }
        public string NumeroConvenio { get; set; }
        public Boolean AceptaPagosDobles { get; set; }
        public string ReferenciaCaptura { get; set; }       
        public string CategoriaComercial { get; set; }
        public int IdAliado { get; set; }
    }
}
