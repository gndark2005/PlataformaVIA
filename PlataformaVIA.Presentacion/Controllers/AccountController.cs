namespace PlataformaVIA.Presentacion.Controllers
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using PlataformaVIA.Core.Domain;
    using PlataformaVIA.Core.Domain.Seguridad;
    using PlataformaVIA.Presentacion.Filters;
    using PlataformaVIA.Presentacion.Helpers;
    using PlataformaVIA.Services.Interfaces;
    using Presentacion.Models;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;


    [Authorize]
    [NoCache]
    public class AccountController : MasterController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private IAdministracionCorreoService _administracionCorreoService;
        private ISeguridadService _SeguridadService;

        public AccountController(IAdministracionCorreoService administracionCorreoService, ISeguridadService seguridadService)
        {
            _administracionCorreoService = administracionCorreoService;
            _SeguridadService = seguridadService;
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
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

        //
        // GET: /Account/Login
        [AllowAnonymous]
        [SecurityLog]
        public ActionResult ErrorPage(Registro obj)
        {
            return PartialView(obj);
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        [NoCache]
        [SecurityLog]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return View("Login", "~/Views/Shared/_PublicLayout.cshtml");
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [NoCache]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, change to shouldLockout: true
                var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: true);

                switch (result)
                {
                    case SignInStatus.Success:

                        var user = await UserManager.FindAsync(model.Email, model.Password);
                        int validacionEmail = 1;
                        using (var context = new ApplicationDbContext())
                        {
                            string resultParameter = context.Database.SqlQuery<String>("exec Parametro_GetParametro @p0, @p1", 0, "ValidacionEmail").FirstOrDefault();
                            if (!string.IsNullOrEmpty(resultParameter))
                            {
                                validacionEmail = Convert.ToInt32(resultParameter);
                            }
                        }

                        if (validacionEmail == 1)
                        {
                            if (!await UserManager.IsEmailConfirmedAsync(user.Id))
                            {
                                ModelState.AddModelError("", "Debe confirmar su correo electronico.");
                                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                                return View(model);
                            }
                        }

                        user.UsuarioInfo.FECHAHORAULTIMOINGRESO = user.UsuarioInfo.FECHAHORAINGRESO;
                        user.UsuarioInfo.FECHAHORAINGRESO = DateTime.Now;

                        RegistroEventos.RegistrarLogin(TipoRegistroEvento.Ingreso, new Exception(), model);

                        //var a = user.UsuarioInfo.UsuariosPuntoDeVenta.First().CODPUNTODEVENTA;

                        await UserManager.UpdateAsync(user);
                        //return RedirectToLocal(returnUrl);
                        //if (String.IsNullOrEmpty(returnUrl))
                        //{

                        
                        
                            return RedirectToLocal(returnUrl);
                            //return RedirectToAction("Login", "Account");
                        
                    //}
                    //else
                    //{
                    //    return RedirectToLocal(returnUrl);
                    //}
                    case SignInStatus.LockedOut:
                        return View("Lockout");
                    case SignInStatus.Failure:
                    default:
                        ModelState.AddModelError("", "Usuario o contraseña incorrecto");
                        return View(model);
                }
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("Login", "Account", exception);
            }
        }

        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await UserManager.FindByNameAsync(model.Email);
                    if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                    {
                        // Don't reveal that the user does not exist or is not confirmed
                        return View("ForgotPasswordConfirmation");
                    }

                    string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                    var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    AdministracionCorreo correo = new AdministracionCorreo { ASUNTO = "Cambio de contraseña", DESTINATARIOS = user.Email, COPIA_DESTINATARIOS = "", MENSAJE = "Ingrese <a href=\"" + callbackUrl + "\">aqui</a> para cambiar su contraseña", TITULO = "Plataforma VIA cambio de contraseña" };
                    ResponseIndividualEO<AdministracionCorreo> correoNuevo = new ResponseIndividualEO<AdministracionCorreo>();
                    correoNuevo.Entidad = correo;
                    _administracionCorreoService.SendEmail(correoNuevo);

                    //await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("ForgotPasswordConfirmation", "Account");
                }

            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }


        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        // GET: /Account/UserConfirmation
        [AllowAnonymous]
        public ActionResult UserConfirmation(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }

            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                await UserManager.AddToPasswordHistoryAsync(user, user.PasswordHash);
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        // POST: /Account/UserConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UserConfirmation(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await UserManager.FindByNameAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "El Correo electrónico ingresado, no está registrado en el sistema. por favor verifique.");
            }
            else
            {
                ValidacionToken validacion = _SeguridadService.ValidarTokenConfirmacion(model.Email, model.Code, false);
                if (validacion.EsValido)
                {
                    var codigoDeAutorizacion = await UserManager.GeneratePasswordResetTokenAsync(user.UsuarioInfo.CODASPNETUSER);
                    var result = await UserManager.ResetPasswordAsync(user.Id, codigoDeAutorizacion, model.Password);
                    if (result.Succeeded)
                    {
                        await UserManager.AddToPasswordHistoryAsync(user, user.PasswordHash);

                        var tokenConfirmation = UserManager.GenerateEmailConfirmationToken(user.Id);
                        UserManager.ConfirmEmail(user.Id, tokenConfirmation);

                        _SeguridadService.ValidarTokenConfirmacion(model.Email, model.Code, true);

                        return RedirectToAction("ConfirmUserPassword", "Account");
                    }
                    AddErrors(result);
                }
                else
                {
                    ModelState.AddModelError("", validacion.Mensaje);
                }
            }
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        // GET: /Account/RegisterConfirmation
        [AllowAnonymous]
        public ActionResult RegisterConfirmation()
        {
            return View();
        }

        // GET: /Account/ConfirmUserPassword
        [AllowAnonymous]
        public ActionResult ConfirmUserPassword()
        {
            return View();
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SecurityLog]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}