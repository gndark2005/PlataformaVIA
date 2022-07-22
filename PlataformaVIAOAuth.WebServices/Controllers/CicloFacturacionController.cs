namespace PlataformaVIAOAuth.WebServices.Controllers
{
    using Newtonsoft.Json;
    using PlataformaVIA.Core.Domain;
    using PlataformaVIA.Core.Domain.Seguridad;
    using PlataformaVIA.Data.Repositories.Implementations;
    using PlataformaVIA.Data.Repositories.Interfaces;
    using PlataformaVIA.Services.Implementations;
    using PlataformaVIA.Services.Interfaces;
    using PlataformaVIAOAuth.WebServices.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;

    [Authorize]
    [RoutePrefix("api/Cadena")]
    public class CicloFacturacionController : ApiController
    {
        private ICicloFacturacionService CicloFacturacion;

        #region Constructores

        public CicloFacturacionController(ICicloFacturacionRepository CicloFacturacionRepository)
        {
           this.CicloFacturacion = new CicloFacturacionService(CicloFacturacionRepository);
        }
        #endregion

        // GET api/Cadena/ConsultarUltimosCiclosFacturacion
        [Route("ConsultarUltimosCiclosFacturacion")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<CicloFacturacion>))]
        public async Task<IHttpActionResult> ConsultarUltimosCiclosFacturacion(bool incluyeUltimoCiclo = false )
        {
            try
            {
                return Ok(this.CicloFacturacion.GetCicloFacturacion(incluyeUltimoCiclo));
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                throw new Exception(exception);
            }
        }
    }
}
