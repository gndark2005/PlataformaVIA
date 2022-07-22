namespace PlataformaVIA.Services.Implementations
{
    using Core.Domain;
    using Core.Domain.ViaBaloto;
    using Data.Repositories.Interfaces;
    using Services.Interfaces;
    using System;
    using System.Collections.Generic;

    public class ViaBalotoService : IViaBalotoService
    {
        public IViaBalotoRepository ViaBalotoRepository { get; }

        public ViaBalotoService( IViaBalotoRepository viabalotoRepository)
        {
            this.ViaBalotoRepository = viabalotoRepository;
        }

        public ResponseEO<Producto> ConsultarProductos(string TextoBusqueda, int NumeroPagina, int RegistrosPorPagina)
        {
            return this.ViaBalotoRepository.ConsultarProductos(TextoBusqueda, NumeroPagina, RegistrosPorPagina);
        }

        public ResponseEO<Producto> ConsultarTodosProductos(string TextoBusqueda, int NumeroPagina, int RegistrosPorPagina)
        {
            return this.ViaBalotoRepository.ConsultarTodosProductos(TextoBusqueda, NumeroPagina, RegistrosPorPagina);
        }

        public ResponseIndividualEO<ProductoDetalle> ConsultarProducto(string ClasificacionProducto, int IdProducto)
        {
            return this.ViaBalotoRepository.ConsultarProducto(ClasificacionProducto, IdProducto);
        }

        public ResponseIndividualEO<ProductoEspecifico> ConsultarProductoGeneral(string ClasificacionProducto, int IdProducto)
        {
            return this.ViaBalotoRepository.ConsultarProductoGeneral(ClasificacionProducto, IdProducto);
        }

        public ResponseEO<PuntoDeVenta> GetPuntosDeVentaCercanos(float latitud, float longitud, int numeroPagina, int registrosPorPagina)
        {
            return this.ViaBalotoRepository.GetPuntosDeVentaCercanos(latitud, longitud, numeroPagina, registrosPorPagina);
        }

        public ResponseEO<PuntoDeVenta> GetPuntosDeVentaCercanosPorProducto(float latitud, float longitud, int numeroPagina, int registrosPorPagina, string clasificacionProducto, int idProducto)
        {
            return this.ViaBalotoRepository.GetPuntosDeVentaCercanosPorProducto(latitud, longitud, numeroPagina, registrosPorPagina, clasificacionProducto, idProducto);
        }


        public ResponseEO<Transaccion> ConsultarTransacciones(string clasificacionProducto, int idProducto, int idCiudad, string textoReferencia, DateTime fechaInicio, DateTime fechaFin, int numeroPagina, int registrosPorPagina, string valor)
        {
            return this.ViaBalotoRepository.ConsultarTransacciones(clasificacionProducto, idProducto, idCiudad, textoReferencia, fechaInicio, fechaFin, numeroPagina, registrosPorPagina, valor);
        }

        public ResponseIndividualEO<TransaccionDetalle> ConsultarTransaccion(long IdTransaccion)
        {
            return this.ViaBalotoRepository.ConsultarTransaccion(IdTransaccion);
        }

        public ResponseEO<Ciudad> ConsultarCiudades(string textoBusqueda, int numeroPagina, int registrosPorPagina)
        {
            return this.ViaBalotoRepository.ConsultarCiudades(textoBusqueda, numeroPagina, registrosPorPagina);
        }

        public Sorteo ConsultarDatosSorteo()
        {
            return this.ViaBalotoRepository.ConsultarDatosSorteo();
        }

        public List<Categoria> GetSubCategorias() 
        {
            return this.ViaBalotoRepository.GetSubCategorias();
        }
        public ResponseEO<Aliado> GetAliadosPorSubcategorias(Int64 IdSubcategoria, Int32 numeroPagina, Int32 registrosPorPagina)
        {
            return this.ViaBalotoRepository.GetAliadosPorSubcategorias(IdSubcategoria, numeroPagina, registrosPorPagina);
        }
        public ResponseEO<Aliado> GetAliadosPorCategorias(string NombreCategoria, Int32 numeroPagina, Int32 registrosPorPagina)
        {
            return this.ViaBalotoRepository.GetAliadosPorCategorias(NombreCategoria, numeroPagina, registrosPorPagina);
        }
        public ResponseEO<Producto> GetProductosPorAliadoCategoria(string NombreCategoria, int IdAliado, Int32 numeroPagina, Int32 registrosPorPagina)
        {
            return this.ViaBalotoRepository.GetProductosPorAliadoCategoria(NombreCategoria, IdAliado, numeroPagina, registrosPorPagina);
        }
        public ResponseEO<Producto> GetProductosPorAliadoSubCategoria(int IdSubCategoria, int IdAliado, Int32 numeroPagina, Int32 registrosPorPagina)
        {
            return this.ViaBalotoRepository.GetProductosPorAliadoSubCategoria(IdSubCategoria, IdAliado, numeroPagina, registrosPorPagina);
        }

        public ResponseEO<Producto> ConsultarProductosGeneral(string TextoBusqueda, int NumeroPagina, int RegistrosPorPagina)
        {
            return this.ViaBalotoRepository.ConsultarProductosGeneral(TextoBusqueda, NumeroPagina, RegistrosPorPagina);
        }

        public ResponseEO<Producto> GetProductosPorSubCategoria(int IdSubCategoria, Int32 numeroPagina, Int32 registrosPorPagina)
        {
            return this.ViaBalotoRepository.GetProductosPorSubCategoria(IdSubCategoria, numeroPagina, registrosPorPagina);
        }
    }
}
