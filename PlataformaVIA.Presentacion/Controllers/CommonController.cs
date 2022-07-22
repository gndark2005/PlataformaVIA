namespace PlataformaVIA.Presentacion.Controllers
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using PlataformaVIA.Core.Domain.Seguridad;
    using PlataformaVIA.Presentacion.Filters;
    using Presentacion.Helpers;
    using Presentacion.Models;
    using System;
    using System.Web;
    using System.Web.Mvc;

    [NoCache]
    [Authorize]
    public class CommonController : Controller
    {
        // GET: Common
        [NoCache]
        public ActionResult HeaderUserInfo()
        {
            try
            {
                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

                if (user != null)
                {
                    return PartialView(user.UsuarioInfo);
                }
                else
                {
                    return PartialView();
                }
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        [NoCache]
        [AllowAnonymous]
        public ActionResult FooterPage()
        {
            return PartialView();
        }

        public ActionResult DescargaTirillaCuadreSemanal(string file)
        {
            try
            {
                if (!System.IO.File.Exists(file))
                {
                    return HttpNotFound();
                }

                var fileBytes = System.IO.File.ReadAllBytes(file);
                var response = new FileContentResult(fileBytes, "application/octet-stream")
                {
                    FileDownloadName = file + ".pdf"
                };
                return response;
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        public ActionResult DescargarArchivo(string file)
        {
            try
            {
                if (!System.IO.File.Exists(file))
                {
                    return HttpNotFound();
                }

                var fileBytes = System.IO.File.ReadAllBytes(file);
                var response = new FileContentResult(fileBytes, "application/octet-stream")
                {
                    FileDownloadName = file + ".pdf"
                };
                return response;
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

    }
}