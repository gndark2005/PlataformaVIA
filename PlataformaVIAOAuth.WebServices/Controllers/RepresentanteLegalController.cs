namespace PlataformaVIAOAuth.WebServices.Controllers
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using PlataformaVIA.Core.Domain;
    using PlataformaVIA.Core.Domain.Busqueda;
    using PlataformaVIA.Core.Domain.Cadena;
    using PlataformaVIA.Core.Domain.Media;
    using PlataformaVIA.Core.Domain.PuntoDeVenta;
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
    [RoutePrefix("api/RepresentanteLegal")]
    public class RepresentanteLegalController : ApiController
    {

        private IPuntoVentaService puntoventaService;
        private ICadenaService cadenaService;

        #region Constructores
        public RepresentanteLegalController(ICadenaRepository CadenaRepository, IPuntoVentaRepository puntoventaRepository)
        {
            this.puntoventaService = new PuntoVentaService(puntoventaRepository);
            this.cadenaService = new CadenaService(CadenaRepository);
        }
        #endregion

        // PUT api/Colocador/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRepresentanteLegal(RazonSocial request)
        {
            try
            {
                return Ok(this.puntoventaService.PutRepresentanteLegal(request));
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
        [Route("ConsultarSaldoFacturacion")]
        [HttpPost]
        [ResponseType(typeof(IEnumerable<SaldoFacturacion>))]
        public async Task<IHttpActionResult> ConsultarSaldoFacturacion(JObject form)
        {
            var nit = string.Empty;
            dynamic jsonObject = form;

            try
            {
                nit = jsonObject.Nit.Value;
            }
            catch (Exception ex)
            {
                return BadRequest("Falta un parámetro");
            }
 
            return Ok(new List<SaldoFacturacion> { new SaldoFacturacion { ConceptoPago = ParticipacionLineaNegocioEnum.Fiducia, FechaLimitePago = DateTime.Now.AddDays(911), SaldoTotalPagar = 75842578 },
                                                    new SaldoFacturacion { ConceptoPago = ParticipacionLineaNegocioEnum.IGT, FechaLimitePago = DateTime.Now.AddDays(910), SaldoTotalPagar = 54787575 }
            });
        }

        // POST api/Account/Register
        [Route("ConsultarNotificaciones")]
        [HttpPost]
        [ResponseType(typeof(IEnumerable<Notificacion>))]
        public async Task<IHttpActionResult> ConsultarNotificaciones(JObject form)
        {
            var Nit = string.Empty;
            dynamic jsonObject = form;

            try
            {
                Nit = jsonObject.Nit.Value;
            }
            catch (Exception ex)
            {
                return BadRequest("Falta un parámetro");
            }

            return Ok(new List<Notificacion> { new Notificacion { Asunto = "Notificación 1" , Detalle = "Detalle de la notificación 1 ", Fecha = DateTime.Now },
                                                    new Notificacion { Asunto = "Notificación 2" , Detalle = "Detalle de la notificación 2 ", Fecha = DateTime.Now }
            });
        }

        // POST api/Account/Register
        [Route("ConsultarNoticias")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<Noticia>))]
        public async Task<IHttpActionResult> ConsultarNoticias()
        {
            return Ok(new List<Noticia> { new Noticia { FechaPublicacion  = DateTime.Now, RutaImagen =  "http:ejemplo/ing/imagen.jpg"},
                                           new Noticia { FechaPublicacion  = DateTime.Now, RutaImagen =  "http:ejemplo/ing/imagen2.jpg" } });
        }

        // POST api/Account/Register
        [Route("ConsultarPuntosdeVenta")]
        [HttpPost]
        [ResponseType(typeof(IEnumerable<PuntodeVenta>))]
        public async Task<IHttpActionResult> ConsultarPuntosdeVenta(ResponseIndividualEO<PuntodeVenta> request)
        {
            try
            {
                return Ok(this.puntoventaService.ConsultarInformacionPuntoDeVenta(request));
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
        [Route("ConsultarCadenas")]
        [HttpPost]
        [ResponseType(typeof(IEnumerable<Cadena>))]
        public async Task<IHttpActionResult> ConsultarCadenas(CriterioBusqueda request)
        {
            try
            {
                return Ok(cadenaService.GetInformacionCadena(request));
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

        /// GET api/Cadena/ConsultarPagosXCicloFacturacion
        /// <summary>
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [Route("ConsultarPagosXCicloFacturacion")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<Pago>))]
        public async Task<IHttpActionResult> ConsultarPagosXCicloFacturacion(ResponseEO<Pago> request)
        {
            try
            {
                return Ok(this.puntoventaService.GetPagosXCicloFacturacion(request));
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

        // GET api/Cadena/ConsultarPagosXCicloFacturacion
        /// <summary>
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [Route("ConsultarAjustesXCicloFacturacion")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<AjusteRazonSocial>))]
        public async Task<IHttpActionResult> ConsultarAjustesXCicloFacturacion(ResponseEO<Ajuste> request)
        {
            try
            {
                return Ok(puntoventaService.GetAjustesXCicloFacturacion(request));
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
    }
}
