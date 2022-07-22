namespace PlataformaVIA.Presentacion.Controllers
{
    using Core.Domain.Seguridad;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using PlataformaVIA.Core.Domain;
    using PlataformaVIA.Presentacion.Filters;
    using PlataformaVIA.Presentacion.Helpers;
    using Presentacion.Models;
    using Presentacion.ViewModels;
    using Presentacion.ViewModels.Registro;
    using Services.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Configuration;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;
    using ResultadoValidacionSeguridad = ViewModels.Registro.ResultadoValidacionSeguridad;
    using UsuarioInfo = ViewModels.UsuarioInfo;

    [Authorize]
    [NoCache]
    [SecurityLog]
    public class SeguridadController : Controller
    {
        private ISeguridadService _seguridadService;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private IAdministracionCorreoService _administracionCorreoService;

        public SeguridadController(ISeguridadService seguridadService, IAdministracionCorreoService administracionCorreoService)
        {
            _seguridadService = seguridadService;
            _administracionCorreoService = administracionCorreoService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> RepresentanteRegistro()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> RepresentanteRegistroDatos(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                var resultadoValidacion = _seguridadService.ValidarTokenDeSeguridad(token);
                if (resultadoValidacion != null && resultadoValidacion.Entidad.Valido == true)
                {
                    ViewBag.Token = resultadoValidacion.Entidad.Token;
                    return View();
                }
            }
            return RedirectToAction("Login", "Account");
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            bool isTakenError = false;
            foreach (var error in result.Errors)
            {
                if (error.Contains("is already taken"))
                {
                    if (isTakenError == false)
                    {
                        ModelState.AddModelError("", "El correo electronico ya se encuentra registrado");
                        isTakenError = true;
                    }
                }
                else
                {
                    ModelState.AddModelError("", error);
                }
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> RepresentanteRegistroDatos(ViewModels.Registro.Registro form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioInfo usuarioInfo = new UsuarioInfo();
                    usuarioInfo.NOMBREUSUARIO = form.NombreCompleto;
                    usuarioInfo.EMAIL = form.Email;
                    usuarioInfo.CELULAR = form.Telefono;
                    int codRazonSocial = _seguridadService.ObtenerRazonSocialPorToken(form.Token);
                    if (codRazonSocial != 0)
                    {
                        usuarioInfo.CODRAZONSOCIAL = codRazonSocial;
                    }
                    /*usuarioInfo.CODROLUSUARIO = 1;*/

                    var user = new ApplicationUser
                    {
                        UserName = form.Email,
                        UsuarioInfo = usuarioInfo,
                        Email = form.Email,
                    };

                    var result = await UserManager.CreateAsync(user, form.Password);
                    if (result.Succeeded)
                    {
                        await UserManager.AddToPasswordHistoryAsync(user, user.PasswordHash);
                        UserManager.AddToRole(user.Id, "Representante Legal");

                        string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        AdministracionCorreo correo = new AdministracionCorreo { ASUNTO = "Registro Plataforma VIA", DESTINATARIOS = user.Email, COPIA_DESTINATARIOS = "", MENSAJE = "Ingrese <a href=\"" + callbackUrl + "\">aqui</a> para confirmar su registo", TITULO = "Plataforma VIA confirmación de registro" };
                        ResponseIndividualEO<AdministracionCorreo> correoNuevo = new ResponseIndividualEO<AdministracionCorreo>();
                        correoNuevo.Entidad = correo;
                        _administracionCorreoService.SendEmail(correoNuevo);

                        return RedirectToAction("RegisterConfirmation", "Account");
                    }

                    AddErrors(result);
                }

                // If we got this far, something failed, redisplay form
                return View(form);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ObtenerPreguntasDeSeguridad(string Nit, string captcharesponse)
        {
            try
            {
                ResponseEO<Pregunta> preguntas = new ResponseEO<Pregunta>();
                if (string.IsNullOrEmpty(captcharesponse))
                {
                    //ModelState.AddModelError("", "La validación no puede estar vacia");
                    //return View(model);
                    return Json(new { Mensaje = new { Error = true, ErrorMessage = "Captcha: La validación no puede estar vacia" } }, JsonRequestBehavior.DenyGet);
                }
                if (!this.CheckCaptcha(captcharesponse))
                {
                    return Json(new { Mensaje = new { Error = true, ErrorMessage = "Captcha: Respuesta incorrecta" } }, JsonRequestBehavior.DenyGet);
                }

                preguntas = _seguridadService.GetPreguntasDeSeguridad(Nit);
                return Json(preguntas, JsonRequestBehavior.DenyGet);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ValidarPreguntasSeguridad(ResultadoValidacionSeguridad resultadoValidacionSeguridad)
        {
            try
            {
                ResponseIndividualEO<ValidacionPreguntasSeguridadResponse> validacionPreguntasSeguridadResponse = new ResponseIndividualEO<ValidacionPreguntasSeguridadResponse>();
                if (resultadoValidacionSeguridad.ValoresContestados.Count == 3)
                {
                    ValidacionPreguntasSeguridad validacionPreguntasSeguridad = new ValidacionPreguntasSeguridad();
                    validacionPreguntasSeguridad.Nit = resultadoValidacionSeguridad.Nit;
                    List<Pregunta> respuestas = new List<Pregunta>();
                    foreach (var respuesta in resultadoValidacionSeguridad.ValoresContestados)
                    {
                        respuestas.Add(new Pregunta { Id = respuesta.CodPregunta, RespuestaSeleccionada = respuesta.RespuestaSeleccionada });
                    }
                    validacionPreguntasSeguridad.PreguntasSeguridad = respuestas;
                    validacionPreguntasSeguridadResponse = _seguridadService.ValidarPreguntasSeguridad(validacionPreguntasSeguridad);
                }
                else
                {
                    validacionPreguntasSeguridadResponse.Mensaje.Error = true;
                    validacionPreguntasSeguridadResponse.Mensaje.ErrorMessage = "No ha seleccionado todas las opciones";
                }
                return Json(validacionPreguntasSeguridadResponse, JsonRequestBehavior.DenyGet);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult MensajeSeguridad(bool esMensajeDeError, string titulo, string mensaje, string destino)
        {
            try
            {
                if (!string.IsNullOrEmpty(titulo) && !string.IsNullOrEmpty(mensaje))
                {
                    if (esMensajeDeError)
                    {
                        ViewBag.TipoMensaje = "alert alert-danger";
                    }
                    else
                    {
                        ViewBag.TipoMensaje = "alert alert-success";
                    }

                    ViewBag.Titulo = titulo;
                    ViewBag.Mensaje = mensaje;

                    switch (destino)
                    {
                        case "Login":
                            ViewBag.Destino = Url.Action("Login", "Account");
                            break;
                        default:
                            ViewBag.Destino = Url.Action("Contact", "Home");
                            break;
                    }

                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        private bool CheckCaptcha(string response)
        {
            //string Response = HttpContext.Current.Request.QueryString["g-recaptcha-response"];//Getting Response String Append to Post Method
            bool Valid = false;
            //Request to Google Server
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create
            (" https://www.google.com/recaptcha/api/siteverify?secret=" + WebConfigurationManager.AppSettings["recaptchaPrivateKey"] + "&response=" + response);
            try
            {
                //Google recaptcha Response
                using (WebResponse wResponse = req.GetResponse())
                {

                    using (StreamReader readStream = new StreamReader(wResponse.GetResponseStream()))
                    {
                        string jsonResponse = readStream.ReadToEnd();

                        JavaScriptSerializer js = new JavaScriptSerializer();
                        CaptchaValidate data = js.Deserialize<CaptchaValidate>(jsonResponse);// Deserialize Json

                        Valid = Convert.ToBoolean(data.success);
                    }
                }

                return Valid;
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                RedirectToAction("ErrorPage", "Account", exception);
                return false;
            }
        }

    }
}