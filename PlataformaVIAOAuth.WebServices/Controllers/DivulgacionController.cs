using Microsoft.AspNet.Identity.Owin;
using PlataformaVIA.Core.Domain;
using PlataformaVIA.Core.Domain.Seguridad;
using PlataformaVIA.Services.Interfaces;
using PlataformaVIAOAuth.WebServices.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace PlataformaVIAOAuth.WebServices.Controllers
{
    //[Authorize]
    [RoutePrefix("api/Divulgacion")]
    public class DivulgacionController : ApiController
    {
        private IDivulgacionService _DivulgacionService;
        private ApplicationUserManager _userManager;       
        private string UrlAPIPlataforma;

        #region "Constructores"

        //public SeguridadController() {
        //}

        public DivulgacionController(IDivulgacionService divulgacionService)
        {
            this._DivulgacionService = divulgacionService;
            
            UrlAPIPlataforma = ConfigurationManager.AppSettings["UrlAPIPlataforma"];
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        #endregion

       
        [AllowAnonymous]
        [Route("ObtenerResultadosDivulgacion")]
        [HttpPost]
        [ResponseType(typeof(IEnumerable<ResultadoDivulgacion>))]
        public async Task<IHttpActionResult> ObtenerResultadosDivulgacion(int idDivulgacion, DateTime fechaInicio, DateTime fechaFin)
        {
            List<ResultadoDivulgacion> resultado = new List<ResultadoDivulgacion>();

            try
            {
                var response = this._DivulgacionService.ObtenerResultadosDivulgacion(idDivulgacion, fechaInicio, fechaFin);

                resultado = response.ToList();

            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                throw new Exception(exception);
            }

            return Ok(resultado);
        }
    }
}