namespace PlataformaVIAOAuth.WebServices.Controllers
{
    using Newtonsoft.Json.Linq;
    using PlataformaVIA.Core.Domain;
    using PlataformaVIA.Core.Domain.Busqueda;
    using PlataformaVIA.Core.Domain.Reportes;
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
    [RoutePrefix("api/ReportePuntoVenta")]
    public class ReportePuntoVentaController : ApiController
    {

        private IPuntoVentaService puntoventaService;

        #region Constructores
        public ReportePuntoVentaController(IPuntoVentaRepository puntoventaRepository)
        {
            this.puntoventaService = new PuntoVentaService(puntoventaRepository);
        }
        #endregion

        [Route("GenerarSolicitudPrefacturacion")]
        [HttpGet]
        [ResponseType(typeof(SolicitudReporteResponse))]
        public async Task<IHttpActionResult> GenerarSolicitudEnvioReportePrefacturacion(CriterioBusqueda request)
        {
            try
            {
                return Ok(puntoventaService.AddSolicitudPrefacturacion(request.IdPadre));
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                throw new Exception(exception);
            }

        }
    }
}
