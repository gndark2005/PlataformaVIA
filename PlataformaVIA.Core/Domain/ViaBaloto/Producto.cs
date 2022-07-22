using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaVIA.Core.Domain.ViaBaloto
{
    public class Producto
    {
        public string ClasificacionProducto { get; set;}
        public long IdProducto { get; set; }
        [Display(Name = "Nombre del producto")]
        public string NombreProducto { get; set; }
        [Display(Name = "Tipo del producto")]
        public string TipoProducto { get; set; }
        public string logo { get; set; }
        public string Descripcion { get; set; }
        public Boolean EsPrincipal { get; set; }

    }
}
