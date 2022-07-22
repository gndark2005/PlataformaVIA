using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaVIA.Core.Domain.Busqueda
{
    public class CriterioBusquedaPagoAjuste
    {
        public int IdPadre { get; set; }
        public int CodUsuario { get; set; }
        public int CodCicloFacturacion { get; set; }
        public int CodTipoFiltro { get; set; }
        public string Valor { get; set; }
        
        public ParametroPaginacion Paginacion { get; set; }
    }
}
