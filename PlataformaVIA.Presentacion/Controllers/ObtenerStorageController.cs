using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PlataformaVIA.Core.Domain.AdministradorDocumentos;
using PlataformaVIA.Core.Domain.Seguridad;
using PlataformaVIA.Presentacion.Helpers;
using PlataformaVIA.Presentacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlataformaVIA.Presentacion.Controllers
{
    public class ObtenerStorageController : Controller
    {
        // GET: ObtenerStorage
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetFile(string ubicacion)
        {
            try
            {
                //ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
                //string decrypt = Cipher.DecryptString(ubicacion, user.Id, true);
                FileConstructor file = AzureStorage.Instance.GetFileFromStoragePublic(ubicacion);
                return File(file.ByteArray, file.TipoArchivo);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }
    }
}