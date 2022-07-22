using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using PlataformaVIA.Core.Domain;
using PlataformaVIA.Core.Domain.Seguridad;
using PlataformaVIA.Data.Repositories.Implementations;
using PlataformaVIA.Data.Repositories.Interfaces;
using PlataformaVIA.Services.Implementations;
using PlataformaVIA.Services.Interfaces;

namespace PlataformaVIAOAuth.WebServices.Helpers
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

        public static string RegistrarEvento(TipoRegistroEvento Tipo, Exception ex)
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
                Usuario = new UsuarioInfo { EMAIL = HttpContext.Current.User.Identity.Name },
                Ip = HttpContext.Current.Request.UserHostAddress.ToString(),
                Navegador = HttpContext.Current.Request.Browser.Browser.ToString() + " " + HttpContext.Current.Request.Browser.Version.ToString(),
                Path = HttpContext.Current.Request.Path.ToString(),
                Mensaje = mensaje,
                inneException = inner,
                FechaEvento = DateTime.Now
            };

            var rest = _registroService.RegistrarEvento(respuesta);

            throw new Exception("Se ha producido un error, Id : " + rest.IdError.ToString());
        }


        public static string RegistrarParametros(TipoRegistroEvento Tipo, string Parametros)
        {
            string inner = "";
            string mensaje = "";
         
            Registro respuesta = new Registro()
            {
                TipoRegistro = Tipo,
                Usuario = new UsuarioInfo { EMAIL = HttpContext.Current.User.Identity.Name },
                Ip = HttpContext.Current.Request.UserHostAddress.ToString(),
                Navegador = HttpContext.Current.Request.Browser.Browser.ToString() + " " + HttpContext.Current.Request.Browser.Version.ToString(),
                Path = HttpContext.Current.Request.Path.ToString(),
                Mensaje = mensaje,
                inneException = inner,
                FechaEvento = DateTime.Now,
                Parametros = Parametros

            };

            var rest = _registroService.RegistrarEvento(respuesta).ToString();

            return rest;
        }
    }
}