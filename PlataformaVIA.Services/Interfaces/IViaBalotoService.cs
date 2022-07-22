namespace PlataformaVIA.Services.Interfaces
{
    using Core.Domain;
    using Core.Domain.ViaBaloto;
    using System;
    using System.Collections.Generic;

    public interface IViaBalotoService
    {
        ResponseEO<Producto> ConsultarProductos(string TextoBusqueda, int NumeroPagina, int RegistrosPorPagina);
        ResponseEO<Producto> ConsultarTodosProductos(string TextoBusqueda, int NumeroPagina, int RegistrosPorPagina);
        ResponseIndividualEO<ProductoDetalle> ConsultarProducto(string ClasificacionProducto, int IdProducto);
        ResponseIndividualEO<ProductoEspecifico> ConsultarProductoGeneral(string ClasificacionProducto, int IdProducto);
        ResponseEO<PuntoDeVenta> GetPuntosDeVentaCercanos(float latitud, float longitud, int numeroPagina, int registrosPorPagina);
        ResponseEO<PuntoDeVenta> GetPuntosDeVentaCercanosPorProducto(float latitud, float longitud, int numeroPagina, int registrosPorPagina, string clasificacionProducto, int idProducto);
        ResponseEO<Transaccion> ConsultarTransacciones(string clasificacionProducto, int idProducto, int idCiudad, string textoReferencia, DateTime fechaInicio, DateTime fechaFin, int numeroPagina, int registrosPorPagina, string valor);
        ResponseIndividualEO<TransaccionDetalle> ConsultarTransaccion(long IdTransaccion);
        ResponseEO<Ciudad> ConsultarCiudades(string textoBusqueda, int numeroPagina, int registrosPorPagina);
        Sorteo ConsultarDatosSorteo();
        List<Categoria> GetSubCategorias();
        ResponseEO<Aliado> GetAliadosPorSubcategorias(Int64 IdSubcategoria,Int32 numeroPagina, Int32 registrosPorPagina);
        ResponseEO<Aliado> GetAliadosPorCategorias(string NombreCategoria, Int32 numeroPagina, Int32 registrosPorPagina);
        ResponseEO<Producto> GetProductosPorAliadoCategoria(string NombreCategoria, int IdAliado, Int32 numeroPagina, Int32 registrosPorPagina);
        ResponseEO<Producto> GetProductosPorAliadoSubCategoria(int IdSubCategoria, int IdAliado, Int32 numeroPagina, Int32 registrosPorPagina);
        ResponseEO<Producto> ConsultarProductosGeneral(string TextoBusqueda, int NumeroPagina, int RegistrosPorPagina);
        ResponseEO<Producto> GetProductosPorSubCategoria(int IdSubCategoria, Int32 numeroPagina, Int32 registrosPorPagina);

    }
}
