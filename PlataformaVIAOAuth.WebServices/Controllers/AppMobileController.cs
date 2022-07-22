namespace PlataformaVIAOAuth.WebServices.Controllers
{
    using PlataformaVIA.Core.Domain.AdministradorDocumentos;
    using PlataformaVIA.Core.Domain.AppMobile;
    using PlataformaVIA.Services.Interfaces;
    using PlataformaVIAOAuth.WebServices.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;

    public class AppMobileController : ApiController
    {
        private IAppMobileService appMobileService;

        public AppMobileController(IAppMobileService _appMobileService)
        {
            appMobileService = _appMobileService;
        }

        #region Productos
        public async Task<IHttpActionResult> GetProductosByFiltro(string filtro)
        {
            var productos = appMobileService.GetProductosByFiltro(filtro);
            return Ok(productos);
        }

        public async Task<IHttpActionResult> GetProducto(int id, string tipo)
        {
            var producto = appMobileService.GetProducto(id, tipo);
            return Ok(producto);
        }

        #endregion
        #region Banners
        public async Task<IHttpActionResult> GetBanner()
        {
            var banners = appMobileService.GetBanner();

            return Ok(banners);
        }
        #endregion

        #region Categorias
        public async Task<IHttpActionResult> GetCategoriasByFechaSincronizacion(DateTime pultimaFechaSincronizacion)
        {
            var categorias = appMobileService.GetCategoriasByFechaSincronizacion(pultimaFechaSincronizacion);
            return Ok(categorias);
        }
        #endregion

        public async Task<IHttpActionResult> GetImagenByCodInstructivoImagen(int CodInstructivoImagen)
        {
            var result = appMobileService.GetImagenByCodInstructivoImagen(CodInstructivoImagen);

            var obj = AzureStorage.Instance.GetFileFromStorage(result.RutaImagen).ByteArray;

            return Ok(obj);
        }

        #region Instructivos

        public async Task<IHttpActionResult> GetInstructivoById(int codInstructivo)
        {
            var instructivos = appMobileService.GetInstructivoById(codInstructivo);
            return Ok(instructivos);
        }

        public async Task<IHttpActionResult> GetInstructivoByTerminalYCategoria(int CodTipoTerminal, int CodCategoria)
        {
            var instructivos = appMobileService.GetInstructivoByTerminalYCategoria(CodTipoTerminal, CodCategoria);

            return Ok(instructivos);
        }

        public async Task<IHttpActionResult> GetInstructivosByTerminal(int tipoTerminal)
        {
            var instructivos = appMobileService.GetInstructivosByTerminal(tipoTerminal);

            return Ok(instructivos);
        }

        public async Task<IHttpActionResult> GetInstructivosByProductoYTerminal(int CodProducto, int tipoTerminal)
        {
            var instructivos = appMobileService.GetInstructivosByProductoYTerminal(tipoTerminal, CodProducto);

            return Ok(instructivos);
        }

        public async Task<IHttpActionResult> GetInstructivos()
        {
            var instructivos = appMobileService.GetInstructivos();

            return Ok(instructivos);
        }

        #endregion

        public async Task<IHttpActionResult> GetNoticia()
        {
            Noticia noticia = appMobileService.GetNoticia();

            return Ok(noticia);
        }

        public async Task<IHttpActionResult> GetPuntosCercanos(double longitud, double latitud)
        {
            var puntosCercanos = appMobileService.GetPuntosCercanos(longitud, latitud);
            return Ok(puntosCercanos);
        }
    }
}