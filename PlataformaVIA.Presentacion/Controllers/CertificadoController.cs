namespace PlataformaVIA.Presentacion.Controllers
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using PlataformaVIA.Core.Domain.AdministradorDocumentos;
    using PlataformaVIA.Core.Domain.Parametro;
    using PlataformaVIA.Core.Domain.Seguridad;
    using PlataformaVIA.Presentacion.Filters;
    using PlataformaVIA.Presentacion.Helpers;
    using PlataformaVIA.Presentacion.Models;
    using PlataformaVIA.Presentacion.ViewModels.Certificados;
    using PlataformaVIA.Services.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Mvc;

    [Authorize]
    [NoCache]
    [SecurityLog]
    public class CertificadoController : Controller
    {
        private ICertificadosService _certificadosService;
        private IParametroService _parametroService;

        #region Constructores

        public CertificadoController(ICertificadosService certificadosService, IParametroService parametroService)
        {
            this._certificadosService = certificadosService;
            this._parametroService = parametroService;
        }
        #endregion

        // GET: Certificado
        [Authorize(Roles = "Representante Legal")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetListadoDeCertificados()
        {
            try
            {
                int codUsuario = CrossController.Instance.GetUserInfoId();

                var tiposCertificado = new GenerarCertificadosViewModel
                {
                    TiposDeCertificadoList = new SelectList(_certificadosService.GetTipoCertificado(codUsuario), "CodigoCertificado", "NombreCertificado")
                };
                return View("_GenerarCertificado", tiposCertificado);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ObtenerDocumento(string id)
        {
            try
            {
                string archivo = _certificadosService.ObtenerRutaDeStoragePorToken(id);
                AzureStorage.Instance.DownloadFileFromStorage(archivo);
                _certificadosService.ActualizarEstadoPorToken(id);
                return RedirectToAction("Contact", "Home");
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ObtenerPoliticas()
        {
            try
            {

                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
                Parametro parametro = _parametroService.BuscarParametro(CrossController.Instance.GetUserInfoId(), "PoliticasTratamientoDatosPersonales");

                AzureStorage.Instance.DownloadFileFromStorage(parametro.VALOR);

                return RedirectToAction("Contact", "Home");
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        public JsonResult _GetFechasCertificado(int codCertificado)
        {
            try
            {
                int codUsuario = CrossController.Instance.GetUserInfoId();
                var objs = _certificadosService.GetFechaCertificado(codCertificado, codUsuario);

                return Json(
                    objs.Select(mm => new
                    {
                        Fecha = mm.Fecha.ToString("ddMMyyyy"),
                        Texto = mm.Texto

                    }), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                RedirectToAction("ErrorPage", "Account", exception);
                return Json(exception.Mensaje);
            }
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<JsonResult> GenerarCertificado(int codCertificado, string fecha, string keyParametro)
        {
            try
            {
                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
                Parametro parametro = _parametroService.BuscarParametro(CrossController.Instance.GetUserInfoId(), keyParametro.Trim().Replace(" ", ""));

                Parametro iniciales = _parametroService.BuscarParametro(CrossController.Instance.GetUserInfoId(), "Caracteres" + keyParametro.Trim().Replace(" ", ""));

                Parametro Caracteres = _parametroService.BuscarParametro(CrossController.Instance.GetUserInfoId(), "Colaboraciónempresarial_CTAPART");

                

                List<string> lt = new List<string>();

                lt.Add(parametro.VALOR + fecha.Remove(0, 2) + "/" + user.UsuarioInfo.RazonSocial.NIT.ToString().Trim() + user.UsuarioInfo.RazonSocial.DIGITOVERIFICACION.ToString().Trim() + iniciales.VALOR + fecha.Trim() + ".pdf");
                lt.Add(parametro.VALOR + fecha.Remove(0, 2) + user.UsuarioInfo.RazonSocial.NIT.ToString().Trim() + user.UsuarioInfo.RazonSocial.DIGITOVERIFICACION.ToString().Trim() + iniciales.VALOR + fecha.Trim() + "_" + Caracteres.VALOR + ".pdf");

                bool exists = false;
                int index = 0;
                var encrypt = new List<Tuple<int, string>>();
                //List<string> encrypt = new List<string>();

                foreach (var obj in lt)
                {

                    exists = await AzureStorage.Instance.VerifyBlobExists(obj);

                    if (exists)
                    {
                        RegistroEventos.RegistroCertificado(TipoRegistroEvento.Certificado, "Descarga " + keyParametro + " " + user.UsuarioInfo.RazonSocial.NIT.ToString().Trim());
                        encrypt.Add(new Tuple<int, string>(index, Cipher.EncryptString(obj, user.Id, true)));
                        index++;
                    }
                }

                if (encrypt.Count > 0)
                    return Json(encrypt, JsonRequestBehavior.AllowGet);
                else
                    return Json(false, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                RedirectToAction("ErrorPage", "Account", exception);
                return Json(exception.Mensaje, JsonRequestBehavior.AllowGet);
            }
        }


        public virtual ActionResult DescargarCertificado(string fileName)
        {
            try
            {
                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

                string decrypt = Cipher.DecryptString(fileName, user.Id, true);
                AzureStorage.Instance.DownloadFileFromStorage(decrypt);
                //RegistroEventos.RegistroCertificado(TipoRegistroEvento.Certificado, "Descarga de certificado");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                RedirectToAction("ErrorPage", "Account", exception);
                return Json(exception.Mensaje, JsonRequestBehavior.AllowGet);
            }
        }

        public virtual ActionResult MostrarCertificado(string fileName)
        {
            try
            {
                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

                string decrypt = Cipher.DecryptString(fileName, user.Id, true);
                AzureStorage.Instance.DownloadFileFromStorage(decrypt);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                RedirectToAction("ErrorPage", "Account", exception);
                return Json(exception.Mensaje, JsonRequestBehavior.AllowGet);
            }
        }

        public virtual ActionResult VistaDocumento(string ubicacion)
        {
            try
            {
                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

                string decrypt = Cipher.DecryptString(ubicacion, user.Id, true);
                FileConstructor file = AzureStorage.Instance.GetFileFromStorage(decrypt);
                //RegistroEventos.RegistroCertificado(TipoRegistroEvento.Certificado, "Descarga de certificado");
                return File(file.ByteArray, file.TipoArchivo);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                RedirectToAction("ErrorPage", "Account", exception);
                return Json(exception.Mensaje, JsonRequestBehavior.AllowGet);
            }
        }
    }
}