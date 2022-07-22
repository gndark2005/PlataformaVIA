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

namespace PlataformaVIAOAuth.WebServices.Controllers
{
    [Authorize]
    [RoutePrefix("api/SolicitudesBatch")]
    public class SolicitudesBatchController : ApiController
    {
        private ISolicitudBatchService SolicitudBatchService;
        
        #region Constructores
        public SolicitudesBatchController(ISolicitudesBatchRepository SolicitudesBatchRepository)
        {
            this.SolicitudBatchService = new SolicitudBatchService(SolicitudesBatchRepository);
        }
        #endregion

        [Route("ConsultarSolicitudesPendientes")]
        [HttpGet]
        [ResponseType(typeof(SolicitudesBatch))]
        public async Task<IHttpActionResult> ConsultarSolicitudesPendientes()
        {
            try
            {
                return Ok(SolicitudBatchService.SolicitudEnvioReporte_ConsultarSolicitudesPendientes());
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                throw new Exception(exception);
            }
        }

        [Route("MarcarSolicitudProcesada")]
        [HttpPost]
        [ResponseType(typeof(int))]
        public async Task<IHttpActionResult> MarcarSolicitudProcesada(SolicitudesBatch request)
        {
            try
            {
                if (request.ID_SOLICITUDENVIOREPORTE <= 0)
                {
                    return BadRequest("El parametro IdSolicitudDeEnvioReporte, no puede estar vacio.");
                }

                return Ok(SolicitudBatchService.SolicitudEnvioReporte_MarcarSolicitudProcesada(request));
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                throw new Exception(exception);
            }
        }

        [Route("AgregarSolicitud")]
        [HttpPut]
        [ResponseType(typeof(int))]
        public async Task<IHttpActionResult> SolicitudEnvioReporte_Agregar(SolicitudesBatch request)
        {
            try
            {
                if (request.CodTipoSolicitudEnvioReporte == TipoSolicitudEnvioReporteEnum.EstadoCuentaporPuntodeventa || request.CodTipoSolicitudEnvioReporte == TipoSolicitudEnvioReporteEnum.Prefacturacionporpuntodeventa)
                {                   
                    if (request.CodPuntoDeVenta == 0)
                        return BadRequest("El parametro CodPuntoDeVenta, no puede estar vacio.");                   
                }
                if (request.CodTipoSolicitudEnvioReporte == TipoSolicitudEnvioReporteEnum.EstadoCuentaporCadena || request.CodTipoSolicitudEnvioReporte == TipoSolicitudEnvioReporteEnum.PrefacturacionporCadena)
                {
                    
                    if (request.CodCadena == 0)
                        return BadRequest("El parametro CodCadena, no puede estar vacio.");
                }

                return Ok(SolicitudBatchService.SolicitudEnvioReporte_Agregar(request));
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                throw new Exception(exception);
            }
        }

    }
}