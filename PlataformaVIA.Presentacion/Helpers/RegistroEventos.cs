using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using PlataformaVIA.Core.Domain;
using PlataformaVIA.Core.Domain.Seguridad;
using PlataformaVIA.Data.Repositories.Implementations;
using PlataformaVIA.Data.Repositories.Interfaces;
using PlataformaVIA.Presentacion.Models;
using PlataformaVIA.Services.Implementations;
using PlataformaVIA.Services.Interfaces;

namespace PlataformaVIA.Presentacion.Helpers
{
    public static class RegistroEventos
    {
        public static IRegistroService _registroService;
        public static IRegistroRepository _registroRepository;

        #region "Constructores"
        static RegistroEventos()
        {
            _registroRepository = new RegistroRepository();
            _registroService = new RegistroService(_registroRepository);
        }
        #endregion

        public static Registro RegistrarEvento(TipoRegistroEvento Tipo, Exception ex)
        {
            string inner = "";
            string mensaje = "";
            if (ex.InnerException != null)
            {
                inner = ex.InnerException.ToString();
            }

            mensaje = ex.Message.ToString();

            Registro respuesta = new Registro()
            {
                TipoRegistro = Tipo,
                Usuario = new UsuarioInfo { EMAIL = CrossController.Instance.GetUserInfoEmail() },
                Ip = HttpContext.Current.Request.UserHostAddress.ToString(),
                Navegador = HttpContext.Current.Request.Browser.Browser.ToString() + " " + HttpContext.Current.Request.Browser.Version.ToString(),
                inneException = inner,
                Path = HttpContext.Current.Request.Path.ToString(),
                Mensaje = mensaje,
                FechaEvento = DateTime.Now
            };

            var rest = _registroService.RegistrarEvento(respuesta);

            return respuesta;
        }

        public static Registro RegistrarLogin(TipoRegistroEvento Tipo, Exception ex, LoginViewModel model)
        {
            string inner = "";
            string mensaje = "Nuevo Ingreso al sistema";
            if (ex.InnerException != null)
            {
                inner = ex.InnerException.ToString();
            }

            //mensaje = ex.Message.ToString();

            Registro respuesta = new Registro()
            {
                TipoRegistro = Tipo,
                Usuario = new UsuarioInfo { EMAIL = model.Email },
                Ip = HttpContext.Current.Request.UserHostAddress.ToString(),
                Navegador = HttpContext.Current.Request.Browser.Browser.ToString() + " " + HttpContext.Current.Request.Browser.Version.ToString(),
                inneException = inner,
                Path = HttpContext.Current.Request.Path.ToString(),
                Mensaje = mensaje,
                FechaEvento = DateTime.Now
            };

            var rest = _registroService.RegistrarEvento(respuesta);

            return respuesta;
        }

        public static void RegistroCertificado(TipoRegistroEvento Tipo, string mensaje)
        {
            Registro respuesta = new Registro()
            {
                TipoRegistro = Tipo,
                Usuario = new UsuarioInfo { EMAIL = CrossController.Instance.GetUserInfoEmail() },
                Ip = HttpContext.Current.Request.UserHostAddress.ToString(),
                Navegador = HttpContext.Current.Request.Browser.Browser.ToString() + " " + HttpContext.Current.Request.Browser.Version.ToString(),
                inneException = "",
                Path = HttpContext.Current.Request.Path.ToString(),
                Mensaje = mensaje,
                FechaEvento = DateTime.Now
            };
            _registroService.RegistrarEvento(respuesta);
        }
    }

}