using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaVIA.Core.Domain.ViaBaloto
{
    public class ProductoLista
    {
        public int TotalRegistros;
        public int TotalPaginas;
        public int PaginaActual;
        public IEnumerable<Producto> ListaProductos;
    }
}
