using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaVIA.Core.Domain.Reportes
{
    public enum TipoReporteEnum
    {
        EstadoCuentaPDVSemanal     = 1,
        EstadoCuentaCadenaSemanal   = 2,
        PrefacturacionPDV    = 3,
        PrefacturacionCadena = 4,
        ReporteVentasporProductoSemanal = 5,
        ReporteVentasporProductoMensual = 6,
        EstadoDeCuentaPDVDiario = 7,
        EstadoDeCuentaCadenaDiario = 8,
        PrefacturacionPDVMensual = 9,
        PrefacturacionCadenaMensual = 10,
        InformeFiscalVentas = 11,
        ReporteVentasDiarias = 12,
    }
}
