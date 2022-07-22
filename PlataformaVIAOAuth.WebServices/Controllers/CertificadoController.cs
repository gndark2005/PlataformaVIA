namespace PlataformaVIAOAuth.WebServices.Controllers
{
    using PlataformaVIA.Core.Domain;
    using PlataformaVIA.Core.Domain.RepresentanteLegal;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;

    [RoutePrefix("api/Certificado")]
    public class CertificadoController : ApiController
    {
        // POST api/Cadena/CadenaInformacionGeneral
        /// <summary>
        /// Método encargado de retornar información completa de la cadena (Información General,  Estado de Cartera por LDN, y Estado Parámetro de Ventas)
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        //[Route("OtorgarAcceso/{accesocolocador}", Name = "OtorgarAcceso")]
        [HttpPost]
        [Route("GenerarSolicitud")]
        [ResponseType(typeof(Response))]
        public async Task<IHttpActionResult> GenerarSolicitud(SolicitudCertificado solicitudcertificado)
        {
            return Ok(new Response
            {
                Exitoso = true,
                Mensaje = "Solicitud Exitosa"
            });
        }
    }
}
