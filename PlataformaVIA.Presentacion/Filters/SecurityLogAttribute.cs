using PlataformaVIA.Core.Domain;
using PlataformaVIA.Core.Domain.Seguridad;
using PlataformaVIA.Data.Repositories.Implementations;
using PlataformaVIA.Data.Repositories.Interfaces;
using PlataformaVIA.Presentacion.Helpers;
using PlataformaVIA.Services.Implementations;
using PlataformaVIA.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace PlataformaVIA.Presentacion.Filters
{
   
    public class SecurityLogAttribute : ActionFilterAttribute
    {

        public static IRegistroService _registroService;
        public static IRegistroRepository _registroRepository;

        #region "Constructores"
        static SecurityLogAttribute()
        {
            _registroRepository = new RegistroRepository();
            _registroService = new RegistroService(_registroRepository);
        }
        #endregion

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string stringParameters = "";

            if (filterContext.ActionParameters != null)
            {
                if (filterContext.ActionParameters.ContainsKey("model") && filterContext.ActionParameters["model"] != null)
                {
                    var model = filterContext.ActionParameters["model"];
                    var propertyInfo = model.GetType().GetProperties();
                    foreach (PropertyInfo pInfo in propertyInfo)
                    {
                        stringParameters = stringParameters + " " + pInfo.Name + ":" + (pInfo.GetValue(model, null));
                    }
                }
                else
                {
                    foreach (var item in filterContext.ActionParameters)
                    {
                        stringParameters = stringParameters + " " + item.Key + ":" + item.Value;
                    }
                }                
            }
            
            if (!filterContext.RouteData.Values.ContainsKey("parametersDB"))
            {
                filterContext.RouteData.Values.Add("parametersDB", stringParameters);
            }
           
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            
            base.OnActionExecuted(filterContext);

            Registro registro = new Registro()
            {
                TipoRegistro = TipoRegistroEvento.Navegacion,
                Usuario = new UsuarioInfo { EMAIL = CrossController.Instance.GetUserInfoEmail() },
                Ip = HttpContext.Current.Request.UserHostAddress.ToString(),
                Navegador = HttpContext.Current.Request.Browser.Browser.ToString() + " " + HttpContext.Current.Request.Browser.Version.ToString(),
                inneException = "",
                Path = filterContext.RouteData.Values["controller"].ToString() + "/" + filterContext.RouteData.Values["action"].ToString(),
                Mensaje = "Navegación",
                FechaEvento = DateTime.Now,
                DispositivoMovil = HttpContext.Current.Request.Browser.IsMobileDevice,
                Parametros = filterContext.RouteData.Values["parametersDB"].ToString()
            };

            var rest = _registroService.RegistrarEvento(registro);
          
           
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }
    }
}