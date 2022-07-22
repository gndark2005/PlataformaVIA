namespace PlataformaVIAOAuth.WebServices.Controllers
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using PlataformaVIA.Core.Domain;
    using PlataformaVIA.Core.Domain.Reportes;
    using PlataformaVIA.Data.Repositories.Implementations;
    using PlataformaVIA.Data.Repositories.Interfaces;
    using PlataformaVIA.Services.Implementations;
    using PlataformaVIA.Services.Interfaces;
    using System;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;

    [Authorize]
    [RoutePrefix("api/ReporteCadena")]
    public class ReporteCadenaController : ApiController
    {
        private IPuntoVentaService puntoventaService;

        #region Constructores
        public ReporteCadenaController(IPuntoVentaRepository puntoventaRepository)
        {
            this.puntoventaService = new PuntoVentaService(puntoventaRepository);
        }
        #endregion

    }
}
