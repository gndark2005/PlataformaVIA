using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaVIA.Core.Domain.Busqueda
{
    public class BusquedaDetallePago
    {
        [DataType(DataType.Currency)]
        [Display(Name = "Valor Otros: ")]
        public decimal ValorOtros { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name = "Valor sin identificar: ")]
        public decimal ValorSinIDentificar { get; set; }
    }
}
