namespace PlataformaVIA.Presentacion.Controllers
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using PlataformaVIA.Core.Domain.Seguridad;
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
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private IAdministracionCorreoService _administracionCorreoService;
        private ITerminalService _terminalService;
        private ISeguridadService _seguridadService;

        public ManageController(IAdministracionCorreoService administracionCorreoService, ITerminalService terminalService, ISeguridadService seguridadService)
        {
            _administracionCorreoService = administracionCorreoService;
            _terminalService = terminalService;
            _seguridadService = seguridadService;
        }

        public ManageController()
        {
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            try
            {
                ViewBag.StatusMessage =
                    message == ManageMessageId.ChangePasswordSuccess ? "La contraseña ha sido cambiada."
                    : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                    : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                    : message == ManageMessageId.Error ? "An error has occurred."
                    : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                    : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                    : "";

                var userId = User.Identity.GetUserId();
                var model = new IndexViewModel
                {
                    HasPassword = HasPassword(),
                    PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
                    TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                    Logins = await UserManager.GetLoginsAsync(userId),
                    BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
                };
                return View(model);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        //
        // GET: /Manage/ChangePassword
        public async Task<ActionResult> ChangePassword()
        {
            try
            {
                var usuario = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                var history = usuario.PasswordHistory.OrderByDescending(x => x.CreatedDate).FirstOrDefault();

                ViewBag.EsPuntoDeVenta = UserManager.IsInRole(usuario.Id, "Punto de Venta");

                if (history != null)
                {
                    using (var context = new ApplicationDbContext())
                    {
                        int diasVencimiento = 90;
                        string resultParameter = context.Database.SqlQuery<String>("exec Parametro_GetParametro @p0, @p1", 0, "DiasVencimientoPassword").FirstOrDefault();
                        if (!string.IsNullOrEmpty(resultParameter))
                        {
                            diasVencimiento = Convert.ToInt32(resultParameter);
                        }
                        ViewBag.DiasVencimiento = diasVencimiento;
                    }
                    
                    var ultimaFechaDeCambioDeClave = usuario.PasswordHistory.OrderByDescending(x => x.CreatedDate).FirstOrDefault().CreatedDate;
                    
                    ViewBag.UltimoCambioDeClave = ultimaFechaDeCambioDeClave;
                }
                else {
                    ViewBag.UltimoCambioDeClave = DateTime.Now;
                }
                
                return View();
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        //[Authorize(Roles = "Punto de Venta")]
        //public async Task<ActionResult> ChangePasswordTeller()
        //{
        //    try
        //    {
        //        var usuario = await UserManager.FindByIdAsync(User.Identity.GetUserId());
        //        var history = usuario.PasswordHistory.OrderByDescending(x => x.CreatedDate).FirstOrDefault();
        //        if (history != null)
        //        {
        //            var ultimaFechaDeCambioDeClave = usuario.PasswordHistory.OrderByDescending(x => x.CreatedDate).FirstOrDefault().CreatedDate;
        //            ViewBag.UltimoCambioDeClave = ultimaFechaDeCambioDeClave;
        //        }
        //        else
        //        {
        //            ViewBag.UltimoCambioDeClave = DateTime.Now;
        //        }

        //        return View();
        //    }
        //    catch (Exception ex)
        //    {
        //        var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
        //        return RedirectToAction("ErrorPage", "Account", exception);
        //    }
        //}

        //
        // POST: /Manage/ChangePassword

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await UserManager.AddToPasswordHistoryAsync(user, user.PasswordHash);
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
                }
                var usuario = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                var ultimaFechaDeCambioDeClave = usuario.PasswordHistory.OrderByDescending(x => x.CreatedDate).FirstOrDefault().CreatedDate;
                ViewBag.UltimoCambioDeClave = ultimaFechaDeCambioDeClave;
                AddErrors(result);
                return View(model);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        public ActionResult GetPartialView(string idlink)
        {
            switch (idlink)
            {
                case "Plataforma":
                    var usuarioPlataforma = UserManager.FindById(User.Identity.GetUserId());
                    var history = usuarioPlataforma.PasswordHistory.OrderByDescending(x => x.CreatedDate).FirstOrDefault();

                    ViewBag.EsPuntoDeVenta = UserManager.IsInRole(usuarioPlataforma.Id, "Punto de Venta");

                    if (history != null)
                    {
                        using (var context = new ApplicationDbContext())
                        {
                            int diasVencimiento = 90;
                            string resultParameter = context.Database.SqlQuery<String>("exec Parametro_GetParametro @p0, @p1", 0, "DiasVencimientoPassword").FirstOrDefault();
                            if (!string.IsNullOrEmpty(resultParameter))
                            {
                                diasVencimiento = Convert.ToInt32(resultParameter);
                            }
                            ViewBag.DiasVencimiento = diasVencimiento;
                        }

                        var ultimaFechaDeCambioDeClave = usuarioPlataforma.PasswordHistory.OrderByDescending(x => x.CreatedDate).FirstOrDefault().CreatedDate;

                        ViewBag.UltimoCambioDeClave = ultimaFechaDeCambioDeClave;
                    }
                    else
                    {
                        ViewBag.UltimoCambioDeClave = DateTime.Now;
                    }


                    return PartialView("_ChangePassword", new ChangePasswordViewModel());
                case "Terminal":
                    return PartialView("_ChangePassTerminal");
                case "Datos":
                    ApplicationUser usuario = UserManager.FindById(User.Identity.GetUserId());
                    return PartialView("_DataUpdate", new DataUpdateViewModels() { EmailAlterno = usuario.UsuarioInfo.EMAILALTERNO, Celular = usuario.UsuarioInfo.CELULAR });
                default: break;
            }
            return PartialView("_ChangePassword");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult _ChangePassword(ChangePasswordViewModel model) {
            object resultado = null;
            if (!ModelState.IsValid)
            {
                resultado = new { Error = true, Mensaje = "Ha ocurrido un error en la validación" };
                return Json(resultado, JsonRequestBehavior.DenyGet);
                
            }
                var result = UserManager.ChangePassword(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = UserManager.FindById(User.Identity.GetUserId());
                if (user != null)
                {
                    UserManager.AddToPasswordHistoryAsync(user, user.PasswordHash);
                }
                resultado = new { Error = false, Mensaje = "Exitoso" };
                return Json(resultado, JsonRequestBehavior.DenyGet);
            }
            else {
                
                resultado = new { Error = true, Mensaje = FirstError(result) };
            }
            
                return Json(resultado, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult _ChangePassTerminal(ChangeTerminalPasswordViewModel model)
        {
            object resultado = null;
            if (!ModelState.IsValid)
            {
                resultado = new { Error = true, Mensaje = "Ha ocurrido un error en la validación" };
            }
            else {
                try
                {
                    int codUsuario = CrossController.Instance.GetUserInfoId();
                    _terminalService.ChangeTerminalPassword(codUsuario, model.OldTerminalPassword, model.NewTerminalPassword);
                    resultado = new { Error = false, Mensaje = "Cambio de terminal guardado correctamente" };
                }
                catch (Exception ex)
                {
                    resultado = new { Error = true, Mensaje = ex.Message };
                }
               
            }

            return Json(resultado, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult _DataUpdate(DataUpdateViewModels model)
        {
            object resultado = null;
            if (!ModelState.IsValid)
            {
                resultado = new { Error = true, Mensaje = "Ha ocurrido un error en la validación" };
            }
            else
            {
                try
                {
                    int codUsuario = CrossController.Instance.GetUserInfoId();
                    _seguridadService.ActualizarDatos(codUsuario, model.EmailAlterno, model.Celular);
                    resultado = new { Error = false, Mensaje = "Información actualizada correctamente" };
                }
                catch (Exception ex)
                {
                    resultado = new { Error = true, Mensaje = ex.Message };
                }
            }

            return Json(resultado, JsonRequestBehavior.DenyGet);
        }

            //
            // GET: /Manage/SetPassword
            public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                    if (result.Succeeded)
                    {
                        var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                        if (user != null)
                        {
                            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        }
                        return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    AddErrors(result);
                }

                // If we got this far, something failed, redisplay form
                return View(model);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
        }

        //
        // GET: /Manage/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            try
            {
                var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
                if (loginInfo == null)
                {
                    return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
                }
                var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
                return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
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

        private string FirstError(IdentityResult result) {
            string error = result.Errors.ToList()[0];
            string resultado = string.Empty;
            switch (error)
            {
                case "Incorrect password.":
                    resultado = "Contraseña incorrecta";
                    break;
                default:
                    resultado = error;
                    break;
            }
            return resultado;
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        #endregion
    }
}