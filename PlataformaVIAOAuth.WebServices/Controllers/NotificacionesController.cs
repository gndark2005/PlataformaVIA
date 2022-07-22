using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
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
using PlataformaVIA.Core.Domain.Busqueda;
using PlataformaVIAOAuth.WebServices.Models;
using System.Net.Http;
using System.Text;
using Message = PlataformaVIAOAuth.WebServices.Models.MessageFireBase;
using System.IO;
using System.Web.Script.Serialization;
using PlataformaVIAOAuth.WebServices.Helpers;
using PlataformaVIA.Core.Domain.Seguridad;

namespace PlataformaVIAOAuth.WebServices.Controllers
{
    [Authorize]
    [RoutePrefix("api/Notificaciones")]
    public class NotificacionesController : ApiController
    {

        private INotificacionesService Notificaciones;
        public static Uri FireBasePushNotificationsURL { get; set; }

        #region "Constructores"
        public NotificacionesController(INotificacionesRepository NotificacionesRepository)
        {
            this.Notificaciones = new NotificacionesService(NotificacionesRepository);
        }
        #endregion


        #region MetodosAPI

        [Route("ConsultarNotificaciones")]
        [HttpGet]
        //[Authorize(Roles = "Admin")]
        [ResponseType(typeof(ResponseEO<Notificaciones>))]
        public async Task<IHttpActionResult> ConsultarNotificaciones(CriterioBusqueda request)
        {
            var respuesta = new ResponseEO<Notificaciones>();
            try
            {
                if (request.IdUsuario == 0 || string.IsNullOrEmpty(request.IdUsuario.ToString()))
                {
                    return BadRequest("El parametro IdUsuario, no puede estar vacio y debe contener un valor correcto.");
                }

                respuesta = this.Notificaciones.GetNotificaciones(request.IdUsuario);

            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                throw new Exception(exception);
            }

            return Ok(respuesta);
        }

        [Route("CrearTokenPorUsuario")]
        [HttpPut]
        [ResponseType(typeof(int))]
        public async Task<IHttpActionResult> CrearTokenPorUsuario(Notificaciones request)
        {
            try
            {
                var response = this.Notificaciones.CrearTokenPorUsuario(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                throw new Exception(exception);
            }
        }

        [Route("MarcarNotificacionLeida")]
        [HttpPut]
        [ResponseType(typeof(int))]
        public async Task<IHttpActionResult> MarcarNotificacionLeida(Notificaciones request)
        {
            try
            {
                var response = this.Notificaciones.CrearTokenPorUsuario(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                throw new Exception(exception);
            }
        }



        [Route("CrearNotificacion")]
        [HttpPost]
        [ResponseType(typeof(int))]
        public async Task<IHttpActionResult> CrearNotificacion(List<Notificaciones> request)
        {
            try
            {
                ResponseEO<Notificaciones> response = new ResponseEO<Notificaciones>();

                //if (string.IsNullOrEmpty(request.TipoIngresoNotificacion.ToString()))
                //{
                //    throw new System.ArgumentException("El tipo de ingreso para la notificacion no puede estar vacia.");
                //}
                //if (string.IsNullOrEmpty(request.TITULO.ToString()))
                //{
                //    throw new System.ArgumentException("El Titulo para la notificacion no puede estar vacia.");
                //}
                //if (string.IsNullOrEmpty(request.MENSAJE.ToString()))
                //{
                //    throw new System.ArgumentException("El Mensaje para la notificacion no puede estar vacia.");
                //}
                //if (string.IsNullOrEmpty(request.AGENTENOTIFICACION.ToString()))
                //{
                //    throw new System.ArgumentException("El Tipo de Agente para la notificacion no puede estar vacia.");
                //}

                foreach (var obj in request)
                {
                    response = this.Notificaciones.CrearNotificacion(obj);

                    if (response.Entidades.Count() > 0)
                    {
                        Notify(response.Entidades.FirstOrDefault().TOKEN, response.Entidades.FirstOrDefault().MENSAJE, response.Entidades.FirstOrDefault().TITULO);
                    }
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                throw new Exception(exception);
            }
        }


        [Route("ActualizarNotificacion")]
        [HttpPut]
        [ResponseType(typeof(int))]
        public async Task<IHttpActionResult> ActualizarNotificacion(Notificaciones request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.ID_NOTIFICACIONUSUARIOINFO.ToString()))
                {
                    throw new System.ArgumentException("El campo ID_NOTIFICACIONUSUARIOINFO, no puede estar vacia.");
                }

                var response = this.Notificaciones.ActualizarNotificacion(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                throw new Exception(exception);
            }
        }

        #endregion



        private bool Notify(string token, string mensaje, string titulo)
        {
            try
            {
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                //serverKey - Key from Firebase cloud messaging server  
                tRequest.Headers.Add(string.Format("Authorization: key={0}", "AIzaSyA5ItOvnc9ChwmsyBYi46q2O1UiVPfKiTg"));
                //Sender Id - From firebase project setting  
                tRequest.Headers.Add(string.Format("Sender: id={0}", "406576105659"));
                tRequest.ContentType = "application/json";
                var payload = new
                {
                    to = token,
                    priority = "high",
                    content_available = true,
                    notification = new
                    {
                        body = mensaje,
                        title = titulo,
                        badge = 1
                    },
                };

                string postbody = JsonConvert.SerializeObject(payload).ToString();
                Byte[] byteArray = Encoding.UTF8.GetBytes(postbody);
                tRequest.ContentLength = byteArray.Length;
                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            if (dataStreamResponse != null) using (StreamReader tReader = new StreamReader(dataStreamResponse))
                                {
                                    String sResponseFromServer = tReader.ReadToEnd();
                                    //result.Response = sResponseFromServer;
                                }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                throw new Exception(exception);
            }

            return false;
        }



    }

}