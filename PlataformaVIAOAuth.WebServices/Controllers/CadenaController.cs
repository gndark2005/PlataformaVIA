namespace PlataformaVIAOAuth.WebServices.Controllers
{
    using PlataformaVIA.Core.Domain;
    using PlataformaVIA.Core.Domain.Busqueda;
    using PlataformaVIA.Core.Domain.Cadena;
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
    public class CadenaController : ApiController
    {
        List<DetalleCupoPuntodeVenta> detallescupotest = new List<DetalleCupoPuntodeVenta>();

        private ICadenaService Cadena;

        #region "Constructores"
        public CadenaController(ICadenaRepository CadenaRepository)
        {
            this.Cadena = new CadenaService(CadenaRepository);
            this.detallescupotest = detallescupotest;
        }
        #endregion

        // POST api/Cadena/CadenaInformacionGeneral
        /// <summary>
        /// Método encargado de retornar información completa de la cadena (Información General,  Estado de Cartera por LDN, y Estado Parámetro de Ventas)
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [Route("ConsultarDetalleCadena")]
        [HttpGet]
        [ResponseType(typeof(Cadena))]
        public async Task<IHttpActionResult> ConsultarDetalleCadenaXId(Cadena request)
        {
            try
            {
                var result = this.Cadena.ConsultarDetalleCadena(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                throw new Exception(exception);
            }
        }

        // GET api/Cadena/ConsultarFacturacionCadenaXCicloFacturacion
        /// <summary>
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [Route("ConsultarFacturacionCadenaXCicloFacturacion")]
        [HttpGet]
        [ResponseType(typeof(Facturacion))]
        public async Task<IHttpActionResult> ConsultarFacturacionCadenaXCicloFacturacion(ResponseEO<Facturacion> request)
        {
            try
            {
                return Ok(this.Cadena.GetFacturacion(request));
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                throw new Exception(exception);
            }
        }

        // GET api/Cadena/ConsultarDetalleCarteraXPuntoVenta
        [Route("ConsultarDetalleCarteraXPuntoVenta")]
        [HttpPost]
        [ResponseType(typeof(IEnumerable<DetalleCarteraPuntoVenta>))]
        public async Task<IHttpActionResult> ConsultarDetalleCarteraXPuntoVenta(ResponseEO<DetalleCarteraPuntoVenta> request)
        {
            try
            {
                return Ok(this.Cadena.GetDetalleCarteraPorPDV(request));
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                throw new Exception(exception);
            }
        }

        // GET api/Cadena/ConsultarDetalleCupoPuntodeVenta
        [Route("ConsultarDetalleCupoPuntodeVenta")]
        [HttpPost]
        [ResponseType(typeof(IEnumerable<DetalleCupoPuntodeVenta>))]
        //public async Task<IHttpActionResult> ConsultarDetalleCupoPuntodeVenta(JObject form)
        public async Task<IEnumerable<DetalleCupoPuntodeVenta>> ConsultarDetalleCupoPuntodeVentaAsync(CriterioBusqueda form)
        {

            return await Task.FromResult(GetAllDetallesCupoPuntodeVenta(form.IdPadre));
        }

        public IEnumerable<DetalleCupoPuntodeVenta> GetAllDetallesCupoPuntodeVenta(int idcadena)
        {
            return detallescupotest; 
        }
    }
}
