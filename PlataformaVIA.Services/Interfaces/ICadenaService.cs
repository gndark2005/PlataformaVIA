﻿namespace PlataformaVIA.Services.Interfaces
{
    using Core.Domain;
    using Core.Domain.Cadena;
    using PlataformaVIA.Core.Domain.Busqueda;
    using PlataformaVIA.Core.Domain.PuntoDeVenta;
    using PlataformaVIA.Core.Domain.Reportes;
    using System.Collections.Generic;

    public interface ICadenaService
    {
        ResponseEO<Cadena> GetCadenaXRazonSocial(ResponseEO<Cadena> response);
        ResponseIndividualEO<Cadena> GetInformacionCadena(CriterioBusqueda request);
        ResponseEO<EstadoPuntoVentaXLineadeNegocio> GetEstadosPuntosDeVentaAsociados(ResponseEO<EstadoPuntoVentaXLineadeNegocio> response);
        Cadena GetDetalleCadena(int id);
        ResponseEO<Facturacion> GetFacturacion(ResponseEO<Facturacion> response);
        Cadena ConsultarDetalleCadena(Cadena response);
        ResponseEO<DetalleCarteraPuntoVenta> GetDetalleCarteraPorPDV(ResponseEO<DetalleCarteraPuntoVenta> response);
        ResponseEO<DetalleCupoPuntodeVenta> GetDetalleCupoPorPDV(ResponseEO<DetalleCupoPuntodeVenta> response);
        IEnumerable<PeriodoReporte> SolicitudEnvioReporteObtenerSemanas();
        IEnumerable<PeriodoReporte> SolicitudEnvioReporteObtenerMeses();
        ResponseEO<Pago> GetMisPagos(ResponseEO<Pago> response);
        ResponseEO<Ajuste> GetMisAjustes(ResponseEO<Ajuste> response);
    }
}
