using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using PlataformaVIA.Services.Interfaces;
using PlataformaVIA.Core.Domain;
using PlataformaVIA.Services.Implementations;
using PlataformaVIA.Data.Repositories.Interfaces;
using PlataformaVIA.Data.Repositories.Implementations;
using System.Web.Http.Description;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net;
using System.Web.Http;
using PlataformaVIAOAuth.WebServices.Helpers;
using PlataformaVIA.Core.Domain.Seguridad;
using System.Drawing;
using System.IO;
using System.Configuration;
using System.Web.Mvc;
using AllowAnonymousAttribute = System.Web.Http.AllowAnonymousAttribute;
using AuthorizeAttribute = System.Web.Http.AuthorizeAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;
using RoutePrefixAttribute = System.Web.Http.RoutePrefixAttribute;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPutAttribute = System.Web.Http.HttpPutAttribute;
using System.ComponentModel.DataAnnotations;

namespace PlataformaVIAOAuth.WebServices.Controllers
{
    [Authorize]
    [RoutePrefix("api/AdministracionCorreo")]
    public class AdministracionCorreoController : ApiController
    {
        private IAdministracionCorreoService AdministracionCorreo;

        #region "Constructores"
        public AdministracionCorreoController(IAdministracionCorreoRepository AdministracionCorreoRepository)
        {
            this.AdministracionCorreo = new AdministracionCorreoService(AdministracionCorreoRepository);
        }
        #endregion


        #region MetodosAPI

        [Route("ConsultarCorreos")]
        [HttpGet]
        [ResponseType(typeof(ResponseEO<AdministracionCorreo>))]
        public async Task<IHttpActionResult> ConsultarCorreos()
        {
            var respuesta = new ResponseEO<AdministracionCorreo>();

            try
            {
                respuesta = this.AdministracionCorreo.GetCorreos();
                respuesta.Mensaje = new Message { Error = false, ErrorMessage = string.Empty };
            }
            catch (ArgumentException aex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, aex);
                throw new Exception(exception);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                throw new Exception(exception);
            }
            return Ok(respuesta);
        }

        [Route("ActualizarEstadoCorreos")]
        [HttpPost]
        [ResponseType(typeof(ResponseEO<AdministracionCorreo>))]
        public async Task<IHttpActionResult> ActualizarEstadoCorreos(JObject form)
        {
            var respuesta = new ResponseEO<AdministracionCorreo>();

            try
            {
                var request = JsonConvert.DeserializeObject<ResponseEO<AdministracionCorreo>>(form.ToString());

                this.AdministracionCorreo.SetEstadoCorreos(request.Entidades.ToList());
            }
            catch (ArgumentException aex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, aex);
                throw new Exception(exception);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                throw new Exception(exception);
            }
            return Ok(respuesta);
        }

        [Route("InsertarCorreos")]
        [HttpPut]
        [ResponseType(typeof(RespuestaSolicitud))]
        public async Task<IHttpActionResult> InsertarCorreos(JObject form)
        {
            RespuestaSolicitud respuesta = new RespuestaSolicitud();
            try
            {
                var request = JsonConvert.DeserializeObject<AdministracionCorreo>(form.ToString());

                if (string.IsNullOrEmpty(request.DESTINATARIOS))
                {
                    throw new System.ArgumentException("El campo Destinatarios no puede estar vacio.", "Error:");
                }
                else
                {
                    string[] emails = request.DESTINATARIOS.Split(',');

                    foreach (var obj in emails)
                    {
                        if (!new EmailAddressAttribute().IsValid(obj))
                            throw new System.ArgumentException("El Email '" + obj + "', no es valido.", "Error:");
                    }
                }

                if (string.IsNullOrEmpty(request.MENSAJE))
                {
                    throw new System.ArgumentException("El campo Mensaje no puede estar vacio.", "Error:");
                }
                else if (string.IsNullOrEmpty(request.ASUNTO))
                {
                    throw new System.ArgumentException("El campo Asunto no puede estar vacio.", "Error:");
                }

                this.AdministracionCorreo.AddCorreoPendiente(request);

                respuesta.Resultado = true;

                return Ok(respuesta);
            }


            catch (ArgumentException ax)
            {
                respuesta.Mensaje = ax.Message;
                respuesta.Resultado = false;
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                respuesta.Resultado = false;
                return Ok(respuesta);
            }
        }

        [AllowAnonymous]
        [Route("InsertarCorreosBase64")]
        [HttpPut]
        [ResponseType(typeof(AdministracionCorreo))]
        public async Task<IHttpActionResult> InsertarCorreosBase64(JObject form)
        {
            RespuestaSolicitud respuesta = new RespuestaSolicitud();
            try
            {
                var request = JsonConvert.DeserializeObject<AdministracionCorreo>(form.ToString());

                if (string.IsNullOrEmpty(request.DESTINATARIOS))
                {
                    throw new System.ArgumentException("El campo Destinatarios no puede estar vacio.", "Error:");
                }
                else
                {
                    string[] emails = request.DESTINATARIOS.Split(',');

                    foreach (var obj in emails)
                    {
                        if (!new EmailAddressAttribute().IsValid(obj))
                            throw new System.ArgumentException("El Email '" + obj + "', no es valido.", "Error:");
                    }
                }

                if (string.IsNullOrEmpty(request.MENSAJE))
                {
                    throw new System.ArgumentException("El campo Mensaje no puede estar vacio.", "Error:");
                }

                else if (string.IsNullOrEmpty(request.ASUNTO))
                {
                    throw new System.ArgumentException("El campo Asunto no puede estar vacio.", "Error:");
                }
                else if (string.IsNullOrEmpty(request.ARCHIVO))
                {
                    throw new System.ArgumentException("El campo Archivo no puede estar vacio.", "Error:");
                }

                string PATH_STORAGE = await AzureStorage.Instance.InsertIntoStorageBase64(request.ARCHIVO, Guid.NewGuid().ToString());

                if (!string.IsNullOrEmpty(PATH_STORAGE))
                {
                    request.PATH_STORAGE = PATH_STORAGE;
                    request.REQUIERE_TOKEN = 1;

                    string UrlAPIPlataforma = ConfigurationManager.AppSettings["UrlAPIPlataforma"];
                    UrlHelper Url = new UrlHelper(HttpContext.Current.Request.RequestContext);

                    var callbackUrl = Url.Content("~/Certificado/ObtenerDocumento/" + "TOKEN_ID");

                    int index = callbackUrl.IndexOf("Certificado");

                    string UrlFinal = UrlAPIPlataforma + callbackUrl.Substring(index);

                    request.MENSAJE = request.MENSAJE + " <br /><a href =\"" + UrlFinal + "\">Descargar Archivo.</a>";

                    this.AdministracionCorreo.AddCorreoPendiente(request);
                }

                respuesta.Resultado = true;

                return Ok(respuesta);
            }
            catch (ArgumentException ax)
            {
                respuesta.Mensaje = ax.Message;
                respuesta.Resultado = false;
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                respuesta.Resultado = false;
                return Ok(respuesta);
            }

        }

        #endregion
    }

}