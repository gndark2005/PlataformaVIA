namespace PlataformaVIA.Services.Interfaces
{
    using Core.Domain;
    using Core.Domain.Busqueda;
    using Core.Domain.Media;
    using Core.Domain.PuntoDeVenta;
    using PlataformaVIA.Core.Domain.AdministradorDocumentos;
    using PlataformaVIA.Core.Domain.Reportes;
    using System.Collections.Generic;

    public interface IPuntoVentaService
    {
        #region "Puntos de venta"
        ResponseEO<PuntodeVenta> ConsultarPuntoDeVenta(ResponseEO<PuntodeVenta> parametros);
        ResponseIndividualEO<PuntodeVenta> ConsultarInformacionPuntoDeVenta(ResponseIndividualEO<PuntodeVenta> parametros);
        ResponseIndividualEO<UsuarioPuntoDeVenta> ConsultarUsuarioPuntoDeVenta(ResponseIndividualEO<UsuarioPuntoDeVenta> parametros);
        ResponseIndividualEO<DetalleProducto> ConsultarDetalleProducto(ResponseIndividualEO<DetalleProducto> parametros);
        #endregion

        IEnumerable<PuntodeVenta> GetPuntoVentaXRazonSocial(CriterioBusqueda filtro);
        PuntodeVenta GetDetallePuntoVenta(int id);
        IEnumerable<PresupuestoXLineaDeNegocio> GetPresupuestoLineaNegocio(int id);
        ResponseEO<PresupuestoXLineaDeNegocio> PresupuestoPorLineaDeNegocio(ResponseEO<PresupuestoXLineaDeNegocio> response);
        ResponseEO<EstadoCuentaXLineaDeNegocio> EstadosDeCuentaPorLineaDeNegocio(ResponseEO<EstadoCuentaXLineaDeNegocio> response);
        ResponseEO<EstadoDeCuentaGeneral> GetEstadoCuentaGeneral(ResponseEO<EstadoDeCuentaGeneral> response);
        ResponseEO<Notificaciones> GetNotificaciones(ResponseEO<Notificaciones> response);
        ResponseEO<ResumenPago> GetResumenPagoTotal(ResponseEO<ResumenPago> response, bool esPagoMinimo);
        ResponseEO<DistribucionDetallePago> GetDetallePago(ResponseEO<DistribucionDetallePago> response);
        ResponseEO<DistribucionDetalleAjuste> GetDetalleAjuste(ResponseEO<DistribucionDetalleAjuste> response);

        IEnumerable<ResumenCuadreSemanal> GetResumenCuadreSemanal(ResumenCuadreSemanal response);
        ResponseEO<ResumenCuadreSemanal> GetResumenVentasAjusteFacturacion(ResponseEO<ResumenCuadreSemanal> response, bool esPagoMinimo);
        ResponseEO<ResumenPagosRetirosSemanaActual> GetResumenPagosRetiros(ResponseEO<ResumenPagosRetirosSemanaActual> response, bool esPagoMinimo);
        ResponseEO<ResumenAjustesCartera> GetResumenAjustesCartera(ResponseEO<ResumenAjustesCartera> response, bool esPagoMinimo);
        IEnumerable<TirillaCuadreSemanal> GetTirillaCuadreSemanal(CriterioBusquedaCicloFacturacion filtro);
        
        //IEnumerable<Ventas> GetVentas(CriterioBusquedaFechas filtro);
        ResponseEO<Transaccion> GetMisTransacciones(ResponseEO<Transaccion> response);
        ResponseEO<Ventas> GetMisVentas(ResponseEO<Ventas> response);
        ResponseEO<Pago> GetMisPagos(ResponseEO<Pago> response);
        ResponseEO<Ajuste> GetMisAjustes(ResponseEO<Ajuste> response);
        ResponseEO<Producto> GetMisProductos(ResponseEO<Producto> response);
        SolicitudReporteResponse SolicitudEnvioReporte(SolicitudReporte request);
        ImagenesInstructivo ConsultarPoliticaPago(int codUsuario, int codPDV);

        #region "Punto de venta"
        IEnumerable<EstadoDeCuentaGeneral> GetDetalleEstadoDeCuenta(ResponseEO<EstadoDeCuentaGeneral> response);
        IEnumerable<ProductoComercial> GetProductosComerciales(CriterioBusqueda filtro);
        IEnumerable<Transaccion> GetTransacciones(CriterioBusquedaTransaccionesPDV filtro);
        IEnumerable<ResumenPago> GetResumenPagoTotal(long id, int ciclo);
        IEnumerable<ResumenCuadreSemanal> GetResumenCuadreSemanalPagoTotal(ResumenCuadreSemanal response);
        IEnumerable<ResumenCuadreSemanal> GetResumenVentasAjustesSemanaActualPagoTotal(ResumenCuadreSemanal response);
        IEnumerable<ResumenPagosRetirosSemanaActual> GetResumenPagosRetirosSemanaActualPagoTotal(CicloFacturacion request);
        IEnumerable<ResumenAjustesCartera> GetResumenAjustesCarteraPagoTotal(ResumenAjustesCartera request);
        IEnumerable<ResumenPago> GetResumenPagoMinimo(ResumenPago request);
        //IEnumerable<Ventas> GetVentas(CriterioBusquedaFechas filtro); DEFINIR SI ES LA MISMA DE ARRIBA
        ResponseEO<Pago> GetPagosXCicloFacturacion(ResponseEO<Pago> response);
        ResponseEO<Ajuste> GetAjustesXCicloFacturacion(ResponseEO<Ajuste> response);
        ResponseEO<Pago> GetPagosXCicloFacturacion(CicloFacturacion response);
        bool PutRepresentanteLegal(RazonSocial response);
        IEnumerable<ProductoComercialDetalle> GetDetalleProductoComercial(int IdProducto);
        int AddSolicitudPrefacturacion(int Id);

        #endregion

    }
}
