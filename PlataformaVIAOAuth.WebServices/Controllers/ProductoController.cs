namespace PlataformaVIAOAuth.WebServices.Controllers
{
    using Newtonsoft.Json.Linq;
    using PlataformaVIA.Core.Domain;
    using PlataformaVIA.Core.Domain.Busqueda;
    using PlataformaVIA.Core.Domain.Seguridad;
    using PlataformaVIA.Data.Repositories.Implementations;
    using PlataformaVIA.Data.Repositories.Interfaces;
    using PlataformaVIA.Services.Implementations;
    using PlataformaVIA.Services.Interfaces;
    using PlataformaVIAOAuth.WebServices.Helpers;
    using System;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;

    [Authorize]
    [RoutePrefix("api/Producto")]
    public class ProductoController : ApiController
    {

        private IPuntoVentaService puntoventaService;
        
        #region Constructores
        public ProductoController(IPuntoVentaRepository puntoventaRepository)
        {
            this.puntoventaService = new PuntoVentaService(puntoventaRepository);
        }
        #endregion

        // POST api/Cadena/CadenaInformacionGeneral
        /// <summary>
        /// Método encargado de retornar información completa de la cadena (Información General,  Estado de Cartera por LDN, y Estado Parámetro de Ventas)
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [Route("ConsultarDetalleProducto")]
        [HttpPost]
        [ResponseType(typeof(ProductoComercialDetalle))]
        public async Task<IHttpActionResult> ConsultarXId(CriterioBusqueda request)
        {
            try
            {
                return Ok(puntoventaService.GetDetalleProductoComercial(request.IdPadre));
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                throw new Exception(exception);
            }
        }
    }
}
