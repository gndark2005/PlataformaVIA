namespace PlataformaVIA.Presentacion.Helpers
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using PlataformaVIA.Data.Repositories.Implementations;
    using PlataformaVIA.Data.Repositories.Interfaces;
    using PlataformaVIA.Services.Implementations;
    using PlataformaVIA.Services.Interfaces;
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    public class PasswordExpiredAuthorizeAttribute : ActionFilterAttribute
    {
        private ISeguridadService _seguridadService;
        private ISeguridadRepository _SeguridadRepository;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            this._SeguridadRepository = new SeguridadRepository();
            this._seguridadService = new SeguridadService(this._SeguridadRepository);

            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var user = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(HttpContext.Current.User.Identity.GetUserId());

                string FechaHoraUltimoCambioPassword = "";
                bool RequiereCambio = _seguridadService.ValidarVencimientoPassword(user.UsuarioInfo.CODASPNETUSER, out FechaHoraUltimoCambioPassword);

                if (RequiereCambio)
                {
                    filterContext.Result = new RedirectToRouteResult(new
                     RouteValueDictionary(new { controller = "Manage", action = "ChangePassword" }));
                }
            }
        }
    }
}