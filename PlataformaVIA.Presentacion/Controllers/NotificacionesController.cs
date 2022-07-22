namespace PlataformaVIA.Presentacion.Controllers
{
    using PlataformaVIA.Core.Domain;
    using PlataformaVIA.Core.Domain.Seguridad;
    using PlataformaVIA.Presentacion.Filters;
    using PlataformaVIA.Presentacion.Helpers;
    using PlataformaVIA.Services.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    [NoCache]
    [SecurityLog]
    public class NotificacionesController : Controller
    {
        private INotificacionesService _notificacionesService;

        #region Constructores
        public NotificacionesController(INotificacionesService notificacionesService)
        {
            this._notificacionesService = notificacionesService;
        }
        #endregion
        // GET: Notificaciones
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult GetPushNotifications()
        {
            try
            {
                var response = _notificacionesService.GetNotificaciones(CrossController.Instance.GetUserInfoId());

                return Json(response.Entidades, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        [HttpPost]
        [Authorize]
        public void SaveToken(string token)
        {
            try
            {
                Notificaciones notificacion = new Notificaciones
                {
                    ID_NOTIFICACIONUSUARIOINFO = CrossController.Instance.GetUserInfoId(),
                    TipoTokenNotificacion = Core.Domain.TipoTokenNotificacionEnum.Browser,
                    TOKENNOTIFICADO = token
                };
                var response = _notificacionesService.CrearTokenPorUsuario(notificacion);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        [HttpPost]
        [Authorize]
        public void MarkAsRead(int idNotification)
        {
            try
            {
                Notificaciones notificacion = new Notificaciones
                {
                    ID_NOTIFICACIONUSUARIOINFO = idNotification,
                };
                var response = _notificacionesService.ActualizarNotificacion(notificacion);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        [Authorize]
        public ActionResult _NotificacionesWindow()
        {
            try
            {
                var response = _notificacionesService.GetNotificaciones(CrossController.Instance.GetUserInfoId());

                return PartialView(response.Entidades);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }
    }
}