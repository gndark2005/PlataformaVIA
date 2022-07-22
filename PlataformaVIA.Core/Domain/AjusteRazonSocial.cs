using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaVIA.Core.Domain
{
    public class AjusteRazonSocial
    {
        public DateTime Fecha { get; set; }
        public decimal Valor { get; set; }
        public TipoAjusteEnum Tipo { get; set; }
        public string Descripcion { get; set; }
        public IEnumerable<DistribucionAjusteRazonSocial> Distribuciones { get; set; }
    }
}
