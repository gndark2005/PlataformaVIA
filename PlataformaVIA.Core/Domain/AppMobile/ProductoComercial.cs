using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaVIA.Core.Domain.AppMobile
{
    public class ProductoComercial
    {
        public Int64 Id { get; set; }
        public string Empresa { get; set; }
        public string Convenio { get; set; }
        public string Aliado { get; set; }
        public string Tipo { get; set; }
        public string Categoria { get; set; }
        public string CodigoConvenio { get; set; }
        public string ClasificacionProducto { get; set; }
    }
}
