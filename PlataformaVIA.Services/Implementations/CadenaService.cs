namespace PlataformaVIA.Services.Implementations
{
    using Core.Domain;
    using Core.Domain.Cadena;
    using Data.Repositories.Interfaces;
    using PlataformaVIA.Core.Domain.Busqueda;
    using PlataformaVIA.Core.Domain.PuntoDeVenta;
    using PlataformaVIA.Core.Domain.Reportes;
    using Services.Interfaces;
    using System;
    using System.Collections.Generic;

    public class CadenaService : ICadenaService
    {
        public ICadenaRepository CadenaRepository { get; }

        public CadenaService(ICadenaRepository cadenaRepository)
        {
            this.CadenaRepository = cadenaRepository;
        }

        public ResponseEO<Cadena> GetCadenaXRazonSocial(ResponseEO<Cadena> response)
        {
            return CadenaRepository.GetCadenasXRazonSocial(response);
        }

        public Cadena GetDetalleCadena(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseIndividualEO<Cadena> GetInformacionCadena(CriterioBusqueda request)
        {
            return CadenaRepository.GetInformacionCadena(request);
        }

        public ResponseEO<EstadoPuntoVentaXLineadeNegocio> GetEstadosPuntosDeVentaAsociados(ResponseEO<EstadoPuntoVentaXLineadeNegocio> response) {
            return CadenaRepository.GetEstadosPuntosDeVentaAsociados(response);
        }

        public ResponseEO<Facturacion> GetFacturacion(ResponseEO<Facturacion> response) {
            return CadenaRepository.GetFacturacion(response);
        }

        public Cadena ConsultarDetalleCadena(Cadena response)
        {
            return CadenaRepository.ConsultarDetalleCadena(response);
        }

        public ResponseEO<DetalleCarteraPuntoVenta> GetDetalleCarteraPorPDV(ResponseEO<DetalleCarteraPuntoVenta> response)
        {
            return CadenaRepository.GetDetalleCarteraPorPDV(response);
        }
        
        public ResponseEO<DetalleCupoPuntodeVenta> GetDetalleCupoPorPDV(ResponseEO<DetalleCupoPuntodeVenta> response)
        {
            return CadenaRepository.GetDetalleCupoPorPDV(response);
        }
        
        public IEnumerable<PeriodoReporte> SolicitudEnvioReporteObtenerSemanas()
        {
            return CadenaRepository.SolicitudEnvioReporteObtenerSemanas();
        }

        public IEnumerable<PeriodoReporte> SolicitudEnvioReporteObtenerMeses()
        {
            return CadenaRepository.SolicitudEnvioReporteObtenerMeses();
        }

        public ResponseEO<Pago> GetMisPagos(ResponseEO<Pago> response)
        {
            return CadenaRepository.GetMisPagos(response);
        }

        public ResponseEO<Ajuste> GetMisAjustes(ResponseEO<Ajuste> response)
        {
            return CadenaRepository.GetMisAjustes(response);
        }
    }
}
