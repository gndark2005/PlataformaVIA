namespace PlataformaVIA.Services.Implementations
{
    using Core.Domain;
    using Core.Domain.Busqueda;
    using Core.Domain.Media;
    using Core.Domain.PuntoDeVenta;
    using Data.Repositories.Interfaces;
    using PlataformaVIA.Core.Domain.AdministradorDocumentos;
    using PlataformaVIA.Core.Domain.Reportes;
    using Services.Interfaces;
    using System.Collections.Generic;

    public class PuntoVentaService : IPuntoVentaService
    {

        public IPuntoVentaRepository PuntoVentaRepository { get; }


        #region "Puntos de venta"
        public ResponseEO<PuntodeVenta> ConsultarPuntoDeVenta(ResponseEO<PuntodeVenta> parametros)
        {
            return PuntoVentaRepository.ConsultarPuntoDeVenta(parametros);
        }

        public ResponseIndividualEO<PuntodeVenta> ConsultarInformacionPuntoDeVenta(ResponseIndividualEO<PuntodeVenta> parametros)
        {
            return PuntoVentaRepository.ConsultarInformacionPuntoDeVenta(parametros);
        }
        
        public ResponseIndividualEO<UsuarioPuntoDeVenta> ConsultarUsuarioPuntoDeVenta(ResponseIndividualEO<UsuarioPuntoDeVenta> parametros)
        {
            return PuntoVentaRepository.ConsultarUsuarioPuntoDeVenta(parametros);
        }

        public ResponseIndividualEO<DetalleProducto> ConsultarDetalleProducto(ResponseIndividualEO<DetalleProducto> parametros)
        {
            return PuntoVentaRepository.ConsultarDetalleProducto(parametros);
        }

        #endregion

        public PuntoVentaService(IPuntoVentaRepository puntoventaRepository)
        {
            this.PuntoVentaRepository = puntoventaRepository;
        }

        public IEnumerable<PuntodeVenta> GetPuntoVentaXRazonSocial(CriterioBusqueda filtro)
        {
            return PuntoVentaRepository.GetPuntoVentaXRazonSocial(filtro);
        }

        public PuntodeVenta GetDetallePuntoVenta(int id)
        {
            return PuntoVentaRepository.GetDetallePuntoVenta(id);
        }

        public IEnumerable<PresupuestoXLineaDeNegocio> GetPresupuestoLineaNegocio(int id)
        {
            return PuntoVentaRepository.GetPresupuestosLineaNegocio(id);
        }

        public ResponseEO<PresupuestoXLineaDeNegocio> PresupuestoPorLineaDeNegocio(ResponseEO<PresupuestoXLineaDeNegocio> response)
        {
            return PuntoVentaRepository.PresupuestoPorLineaDeNegocio(response);
        }

        public ResponseEO<EstadoCuentaXLineaDeNegocio> EstadosDeCuentaPorLineaDeNegocio(ResponseEO<EstadoCuentaXLineaDeNegocio> response)
        {
            return PuntoVentaRepository.EstadosDeCuentaPorLineaDeNegocio(response);
        }

        public ResponseEO<Notificaciones> GetNotificaciones(ResponseEO<Notificaciones> response)
        {
            return PuntoVentaRepository.GetNotificaciones(response);
        }

        public ResponseEO<ResumenPago> GetResumenPagoTotal(ResponseEO<ResumenPago> response, bool esPagoMinimo)
        {
            return PuntoVentaRepository.GetResumenPagoTotal(response, esPagoMinimo);
        }

        public IEnumerable<ResumenCuadreSemanal> GetResumenCuadreSemanal(ResumenCuadreSemanal response)
        {
            return PuntoVentaRepository.GetResumenCuadreSemanal(response);
        }

        public ResponseEO<ResumenCuadreSemanal> GetResumenVentasAjusteFacturacion(ResponseEO<ResumenCuadreSemanal> response, bool esPagoMinimo)
        {
            return PuntoVentaRepository.GetResumenVentasAjusteFacturacion(response, esPagoMinimo);
        }

        public ResponseEO<ResumenPagosRetirosSemanaActual> GetResumenPagosRetiros(ResponseEO<ResumenPagosRetirosSemanaActual> response, bool esPagoMinimo)
        {
            return PuntoVentaRepository.GetResumenPagosRetiros(response, esPagoMinimo);
        }


        public ResponseEO<ResumenAjustesCartera> GetResumenAjustesCartera(ResponseEO<ResumenAjustesCartera> response, bool esPagoMinimo)
        {
            return PuntoVentaRepository.GetResumenAjustesCartera(response, esPagoMinimo);
        }

        public IEnumerable<TirillaCuadreSemanal> GetTirillaCuadreSemanal(CriterioBusquedaCicloFacturacion filtro)
        {
            return PuntoVentaRepository.GetTirillaCuadreSemanal(filtro);
        }

        public ResponseEO<Ventas> GetMisVentas(ResponseEO<Ventas> response)
        {
            return PuntoVentaRepository.GetMisVentas(response);
        }

        public ResponseEO<EstadoDeCuentaGeneral> GetEstadoCuentaGeneral(ResponseEO<EstadoDeCuentaGeneral> response)
        {
            return PuntoVentaRepository.GetEstadoCuentaGeneral(response);
        }

        public ResponseEO<Transaccion> GetMisTransacciones(ResponseEO<Transaccion> response)
        {
            return PuntoVentaRepository.GetMisTransacciones(response);
        }

        public ResponseEO<Producto> GetMisProductos(ResponseEO<Producto> response)
        {
            return PuntoVentaRepository.GetMisProductos(response);
        }

        public ResponseEO<Pago> GetMisPagos(ResponseEO<Pago> response)
        {
            return PuntoVentaRepository.GetMisPagos(response);
        }
       
        public ResponseEO<Ajuste> GetMisAjustes(ResponseEO<Ajuste> response)
        {
            return PuntoVentaRepository.GetMisAjustes(response);
        }
        
        public ResponseEO<DistribucionDetallePago> GetDetallePago(ResponseEO<DistribucionDetallePago> response)
        {
            return PuntoVentaRepository.GetDetallePago(response);
        }

        public ResponseEO<DistribucionDetalleAjuste> GetDetalleAjuste(ResponseEO<DistribucionDetalleAjuste> response)
        {
            return PuntoVentaRepository.GetDetalleAjuste(response);
        }
        
        public SolicitudReporteResponse SolicitudEnvioReporte(SolicitudReporte request)
        {
            return PuntoVentaRepository.SolicitudEnvioReporte(request);
        }

        public ImagenesInstructivo ConsultarPoliticaPago(int codUsuario, int codPDV)
        {
            return PuntoVentaRepository.ConsultarPoliticaPago(codUsuario, codPDV);
        }
        #region "Punto de venta"

        public IEnumerable<EstadoDeCuentaGeneral> GetDetalleEstadoDeCuenta(ResponseEO<EstadoDeCuentaGeneral> response)
        {
            return PuntoVentaRepository.GetDetalleEstadoDeCuenta(response);
        }

        public IEnumerable<ProductoComercial> GetProductosComerciales(CriterioBusqueda filtro)
        {
            return PuntoVentaRepository.GetProductosComerciales(filtro);
        }

        public IEnumerable<Transaccion> GetTransacciones(CriterioBusquedaTransaccionesPDV filtro)
        {
            return PuntoVentaRepository.GetTransacciones(filtro);
        }

        public IEnumerable<ResumenPago> GetResumenPagoTotal(long id, int ciclo)
        {
            return PuntoVentaRepository.GetResumenPagoTotal(id, ciclo);
        }

        public IEnumerable<ResumenCuadreSemanal> GetResumenCuadreSemanalPagoTotal(ResumenCuadreSemanal response)
        {
            return PuntoVentaRepository.GetResumenCuadreSemanalPagoTotal(response);
        }

        public IEnumerable<ResumenCuadreSemanal> GetResumenVentasAjustesSemanaActualPagoTotal(ResumenCuadreSemanal response)
        {
            return PuntoVentaRepository.GetResumenVentasAjustesSemanaActualPagoTotal(response);
        }

        public IEnumerable<ResumenPagosRetirosSemanaActual> GetResumenPagosRetirosSemanaActualPagoTotal(CicloFacturacion request)
        {
            return PuntoVentaRepository.GetResumenPagosRetirosSemanaActualPagoTotal(request);
        }

        public IEnumerable<ResumenAjustesCartera> GetResumenAjustesCarteraPagoTotal(ResumenAjustesCartera request)
        {
            return PuntoVentaRepository.GetResumenAjustesCarteraPagoTotal(request);
        }

        public IEnumerable<ResumenPago> GetResumenPagoMinimo(ResumenPago request)
        {
            return PuntoVentaRepository.GetResumenPagoMinimo(request);
        }

        public ResponseEO<Pago> GetPagosXCicloFacturacion(ResponseEO<Pago> response)
        {
            return PuntoVentaRepository.GetPagosXCicloFacturacion(response);
        }

        public ResponseEO<Ajuste> GetAjustesXCicloFacturacion(ResponseEO<Ajuste> response)
        {
            return PuntoVentaRepository.GetAjustesXCicloFacturacion(response);
        }

        public ResponseEO<Pago> GetPagosXCicloFacturacion(CicloFacturacion response)
        {
            return PuntoVentaRepository.GetPagosXCicloFacturacion(response);
        }

        public bool PutRepresentanteLegal(RazonSocial response)
        {
            return PuntoVentaRepository.PutRepresentanteLegal(response);
        }

        public IEnumerable<ProductoComercialDetalle> GetDetalleProductoComercial(int IdProducto)
        {
            return PuntoVentaRepository.GetDetalleProductoComercial(IdProducto);
        }

        public int AddSolicitudPrefacturacion(int Id)
        {
            return PuntoVentaRepository.AddSolicitudPrefacturacion(Id);
        }

        #endregion
    }
}
