using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaVIA.Core.Domain.Busqueda
{
    public class CriterioBusquedaTransaccionesPDV
    {
        public int IdPadre { get; set; }
        public string Filtro { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public ParametroPaginacion Paginacion { get; set; }
        public int idLineaNegocio { get; set; }
    }
}
