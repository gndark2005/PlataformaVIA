namespace PlataformaVIAOAuth.WebServices.Controllers
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using PlataformaVIA.Core.Domain;
    using PlataformaVIA.Core.Domain.Busqueda;
    using PlataformaVIA.Core.Domain.RepresentanteLegal;
    using PlataformaVIA.Core.Domain.Seguridad;
    using PlataformaVIA.Data.Repositories.Implementations;
    using PlataformaVIA.Data.Repositories.Interfaces;
    using PlataformaVIA.Services.Implementations;
    using PlataformaVIA.Services.Interfaces;
    using PlataformaVIAOAuth.WebServices.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;

    [Authorize]
    [RoutePrefix("api/Colocador")]
    public class ColocadorController : ApiController
    {
        private IColocadorService Colocador;
        
        #region "Constructores"
        public ColocadorController(IColocadorRepository ColocadorRepository)
        {
            this.Colocador = new ColocadorService(ColocadorRepository);
        }
        #endregion



        // POST api/Colocador/ConsultarColocadorXId
        /// <summary>
        /// Método encargado de retornar información completa de la cadena (Información General,  Estado de Cartera por LDN, y Estado Parámetro de Ventas)
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [Route("ConsultarXId")]
        [HttpPost]
        [ResponseType(typeof(Colocador))]
        public async Task<IHttpActionResult> ConsultarXId(CriterioBusqueda request)
        {
            try
            {
                return Ok(this.Colocador.GetDetalleColocador(request));
            }
            catch (Exception ex)
            {
                return BadRequest("Falta un parámetro");
            }
        }

        // POST api/Cadena/CadenaInformacionGeneral
        /// <summary>
        /// Método encargado de retornar información completa de la cadena (Información General,  Estado de Cartera por LDN, y Estado Parámetro de Ventas)
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        //[Route("OtorgarAcceso/{accesocolocador}", Name = "OtorgarAcceso")]
        [HttpPost]
        [ResponseType(typeof(AccesoColocador))]
        public IHttpActionResult OtorgarAcceso(AccesoColocador accesocolocador)
        {
            //TODO Lógica de negocio para Obtener Cadena de acuerdo a su ID

            // TODO implementar cuando ya haya una clase que consulte la BD https://stackoverflow.com/questions/23898846/return-complex-object-in-rest-web-api-call
            //var myObjectResponse = GetObjectFromDb(id);

            //if (myObjectResponse == null)
            //    return Request.CreateResponse(HttpStatusCode.NotFound);
            this.Colocador.OtorgarAcceso(accesocolocador);
            //accesocolocador.IdAccesoColocador = 1;
            return CreatedAtRoute("DefaultApi", new { id = accesocolocador.IdAccesoColocador }, accesocolocador);
            //TODO Cambiar por objeto de BD
            //simular creacióbn y retorno de id 


        }

        // PUT api/Colocador/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutColocador(Colocador request)
        {
            try
            {
                return Ok(this.Colocador.EditColocador(request, 0));
            }
            catch (ArgumentException aex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, aex);
                throw new Exception(exception);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                throw new Exception(exception);
            }
        }

        // POST api/Account/Register
        [Route("ConsultarColocadores")]
        [HttpPost]
        [ResponseType(typeof(IEnumerable<Colocador>))]
        public async Task<IHttpActionResult> ConsultarColocadores(CriterioBusqueda filtro)
        {
            return Ok(Colocador.GetDetalleColocador(filtro));
        }

        // DELETE: api/Colocador/7
        [ResponseType(typeof(Colocador))]
        public async Task<IHttpActionResult> DeleteColocador(CriterioBusqueda request)
        {
            try
            {
                return Ok(this.Colocador.DeleteColocador(request));
            }
            catch (Exception ex)
            {
                return BadRequest("Falta un parámetro");
            }

        }

        [Route("ConsultarColocadoresParaSincronizarSAG")]
        [Authorize( Roles ="Admin")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<Colocador>))]
        public async Task<IHttpActionResult> ConsultarColocadoresParaSincronizarSAG() {
            ResponseEO<Colocador> response = new ResponseEO<Colocador>();
            try
            {
                response = this.Colocador.ConsultarColocadoresParaSincronizarSAG(response);
                return Ok(response.Entidades);
            }
            catch (Exception ex)
            {
                return BadRequest("Ha ocurrido un error: " + ex.Message);
            }
        }

        [Route("ConfirmarSincronizacion")]
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [ResponseType(typeof(Colocador))]
        public async Task<IHttpActionResult>ConfirmarSincronizacion(List<int> objColocador)
        {
            try
            {
                var response = this.Colocador.ConfirmarSincronizacionConSAG(objColocador);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest("Ha ocurrido un error: " + ex.Message);
            }
        }
    }
}
