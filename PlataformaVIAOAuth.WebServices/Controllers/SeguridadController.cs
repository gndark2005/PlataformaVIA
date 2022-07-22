namespace PlataformaVIAOAuth.WebServices.Controllers
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Newtonsoft.Json.Linq;
    using PlataformaVIA.Core.Domain;
    using PlataformaVIA.Core.Domain.Seguridad;
    using PlataformaVIA.Services.Interfaces;
    using PlataformaVIAOAuth.WebServices.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.Description;
    using System.Web.Mvc;
    using WebServices.Models;
    using AllowAnonymousAttribute = System.Web.Http.AllowAnonymousAttribute;
    using AuthorizeAttribute = System.Web.Http.AuthorizeAttribute;
    using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
    using RouteAttribute = System.Web.Http.RouteAttribute;
    using RoutePrefixAttribute = System.Web.Http.RoutePrefixAttribute;
    using UsuarioInfo = PlataformaVIA.Core.Domain.Seguridad.UsuarioInfo;

    //[Authorize]
    [RoutePrefix("api/Seguridad")]
    public class SeguridadController : ApiController
    {
        private ISeguridadService _SeguridadService;
        //private INotificacionesService _NotificacionesService;
        private ApplicationUserManager _userManager;
        private IAdministracionCorreoService _administracionCorreoService;
        private string UrlAPIPlataforma;

        #region "Constructores"

        //public SeguridadController() {
        //}

        public SeguridadController(ISeguridadService seguridadService, IAdministracionCorreoService administracionCorreoService)//, INotificacionesService notificacionesService)
        {
            this._SeguridadService = seguridadService;
            this._administracionCorreoService = administracionCorreoService;
            //this._NotificacionesService = notificacionesService;

            UrlAPIPlataforma = ConfigurationManager.AppSettings["UrlAPIPlataforma"];
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        #endregion

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("ConsultarPreguntasSeguridad")]
        [HttpPost]
        [ResponseType(typeof(IEnumerable<Pregunta>))]
        public async Task<IHttpActionResult> ConsultarPreguntasSeguridad(JObject form)
        {
            dynamic jsonobject = form;
            string NIT = jsonobject.Nit;
            IEnumerable<Pregunta> resultado;

            try
            {
                var response = this._SeguridadService.GetPreguntasDeSeguridad(NIT);

                if (!string.IsNullOrEmpty(response.Mensaje.ErrorMessage))
                {
                    return BadRequest(response.Mensaje.ErrorMessage);
                }
                else
                {
                    resultado = response.Entidades;
                }

            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                throw new Exception(exception);
            }

            return Ok(resultado);
        }


        [AllowAnonymous]
        [Route("ValidarPreguntasSeguridad")]
        [ResponseType(typeof(ValidacionPreguntasSeguridadResponse))]
        [HttpPost]
        public async Task<IHttpActionResult> ValidarPreguntasSeguridad(ResultadoValidacionSeguridad resultadoValidacionSeguridad)
        {
            ResponseIndividualEO<ValidacionPreguntasSeguridadResponse> validacionPreguntasSeguridadResponse = new ResponseIndividualEO<ValidacionPreguntasSeguridadResponse>();

            try
            {
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
                    validacionPreguntasSeguridadResponse = this._SeguridadService.ValidarPreguntasSeguridad(validacionPreguntasSeguridad);
                }
                else
                {
                    validacionPreguntasSeguridadResponse.Mensaje.Error = true;
                    validacionPreguntasSeguridadResponse.Mensaje.ErrorMessage = "No ha seleccionado todas las opciones";
                }
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                throw new Exception(exception);
            }

            return Ok(validacionPreguntasSeguridadResponse);
        }

        // POST api/Account/Register
        [Route("CreacionUsuario")]
        [Authorize(Roles = "Admin")]
        [ResponseType(typeof(Respuesta))]
        [HttpPost]
        public async Task<IHttpActionResult> CreacionUsuario(UsuarioInfo request)
        {
            Response response = new Response();
            try
            {
                var ApplicationUser = new ApplicationUser();

                if (string.IsNullOrEmpty(request.EMAIL))
                {
                    response.Mensaje = "El parametro Email no puede estar vacio.";
                }
                else if (string.IsNullOrEmpty(request.TOKEN))
                {
                    response.Mensaje = "El parametro Token no puede estar vacio.";
                }
                else if (string.IsNullOrEmpty(request.NOMBREUSUARIO))
                {
                    response.Mensaje = "El parametro de Nombre de Usuario no puede estar vacio.";
                }
                else if (string.IsNullOrEmpty(request.TELEFONO))
                {
                    response.Mensaje = "El parametro Telefono no puede estar vacio.";
                }
                else if (string.IsNullOrEmpty(request.CELULAR))
                {
                    response.Mensaje = "El parametro Celular no puede estar vacio.";
                }
                else
                {
                    ResponseIndividualEO<ValidacionTokenResponse> obj = this._SeguridadService.ValidarTokenDeSeguridad(request.TOKEN);

                    if (obj.Entidad.Valido)
                    {
                        UsuarioInfo user = this._SeguridadService.GetUsuarioInfo(request.EMAIL);

                        if (string.IsNullOrEmpty(user.NOMBREUSUARIO))
                        {
                            ApplicationUser = new ApplicationUser
                            {
                                Email = request.EMAIL,
                                EmailConfirmed = false,
                                PasswordHash = "RFb3qx",
                                SecurityStamp = "RFb3qx",
                                PhoneNumber = request.TELEFONO,
                                PhoneNumberConfirmed = false,
                                TwoFactorEnabled = false,
                                LockoutEnabled = false,
                                AccessFailedCount = 0,
                                UserName = request.EMAIL
                            };

                            ApplicationUser.UsuarioInfo = new ViewModels.UsuarioInfo
                            {
                                CODPAIS = request.CODPAIS,
                                //CODROLUSUARIO = request.CODROLUSUARIO,
                                ACTIVO = false,
                                EMAIL = request.EMAIL,
                                EMAILCONFIRMADO = false,
                                CELULAR = request.CELULAR,
                                TELEFONO = request.TELEFONO,
                                NOMBREUSUARIO = request.NOMBREUSUARIO,
                                FECHAHORACREACION = DateTime.Now,
                                CODRAZONSOCIAL = obj.Entidad.CodRazonSocial
                            };
                        }

                        var result = await UserManager.CreateAsync(ApplicationUser, "Ab.12345678910");

                        if (result.Succeeded)
                        {
                            var UserSucceedd = UserManager.FindByEmail(user.EMAIL);

                            var resu = await UserManager.AddToRoleAsync(UserSucceedd.Id, user.ROLE);

                            UrlHelper Url = new UrlHelper(HttpContext.Current.Request.RequestContext);

                            string code = await UserManager.GeneratePasswordResetTokenAsync(UserSucceedd.UsuarioInfo.CODASPNETUSER);

                            var callbackUrl = Url.Action("UserConfirmation", "Account", new { userId = UserSucceedd.Id, code = code });

                            int index = callbackUrl.IndexOf("Account");

                            string UrlFinal = UrlAPIPlataforma + callbackUrl.Substring(index);

                            AdministracionCorreo correo = new AdministracionCorreo { ASUNTO = "Verificacion Cuenta", DESTINATARIOS = UserSucceedd.Email, COPIA_DESTINATARIOS = "", MENSAJE = "Ingrese <a href=\"" + UrlFinal + "\">aqui</a> para la confirmacion de su cuenta.", TITULO = "Plataforma VIA Confirmación de Usuario" };

                            ResponseIndividualEO<AdministracionCorreo> correoNuevo = new ResponseIndividualEO<AdministracionCorreo>();
                            correoNuevo.Entidad = correo;
                            _administracionCorreoService.SendEmail(correoNuevo);

                            response.Exitoso = result.Succeeded;
                        }
                    }
                    else
                    {
                        response.Exitoso = false;
                        response.Mensaje = obj.Mensaje.ErrorMessage;
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                throw new Exception(exception);
            }

            return Ok(response);
        }

        [Route("SincronizacionUsuario")]
        //[Authorize(Roles = "Admin")]
        [ResponseType(typeof(Respuesta))]
        [HttpPost]
        public async Task<IHttpActionResult> SincronizacionUsuario(List<UsuarioInfo> ltUsuario)
        {
            Response response = new Response();
            var ApplicationUser = new ApplicationUser();
            var userContext = new ApplicationDbContext();


            foreach (var obj in ltUsuario)
            {
                try
                {
                    var User = UserManager.FindByEmail(obj.EMAIL);

                    if (User == null)
                    {
                        ApplicationUser = new ApplicationUser
                        {
                            Email = obj.EMAIL,
                            EmailConfirmed = false,
                            PasswordHash = "RFb3qx",
                            SecurityStamp = "RFb3qx",
                            PhoneNumber = obj.TELEFONO,
                            PhoneNumberConfirmed = false,
                            TwoFactorEnabled = false,
                            LockoutEnabled = false,
                            AccessFailedCount = 0,
                            UserName = obj.EMAIL,
                        };

                        ApplicationUser.UsuarioInfo = new ViewModels.UsuarioInfo
                        {
                            ACTIVO = obj.ACTIVO,
                            EMAIL = obj.EMAIL,
                            EMAILCONFIRMADO = false,
                            CELULAR = obj.CELULAR,
                            TELEFONO = obj.TELEFONO,
                            NOMBREUSUARIO = obj.NOMBREUSUARIO,
                            FECHAHORACREACION = DateTime.Now
                        };

                        var result = await UserManager.CreateAsync(ApplicationUser, "Ab.12345678910");

                        if (result.Succeeded)
                        {
                            var UserSucceedd = UserManager.FindByEmail(obj.EMAIL);

                            var resu = await UserManager.AddToRoleAsync(UserSucceedd.Id, obj.ROLE);

                            UrlHelper Url = new UrlHelper(HttpContext.Current.Request.RequestContext);

                            string code = _SeguridadService.ObtenerTokenConfirmacion(UserSucceedd.UsuarioInfo.ID_USUARIOINFO);

                            if (string.IsNullOrEmpty(code)) {
                                throw new Exception("Ha ocurrido un error en la generación del token 'ObtenerTokenConfirmacion'");
                            }
                            
                            var callbackUrl = Url.Action("UserConfirmation", "Account", new { userId = UserSucceedd.Id, code = code });

                            int index = callbackUrl.IndexOf("Account");

                            string UrlFinal = UrlAPIPlataforma + callbackUrl.Substring(index);

                            AdministracionCorreo correo = new AdministracionCorreo { ASUNTO = "Verificacion Cuenta", DESTINATARIOS = UserSucceedd.Email, COPIA_DESTINATARIOS = "", MENSAJE = "Ingrese <a href=\"" + UrlFinal + "\">aqui</a> para la confirmacion de su cuenta.", TITULO = "Plataforma VIA Confirmación de Usuario" };

                            ResponseIndividualEO<AdministracionCorreo> correoNuevo = new ResponseIndividualEO<AdministracionCorreo>();
                            correoNuevo.Entidad = correo;
                            _administracionCorreoService.SendEmail(correoNuevo);

                            response.Exitoso = result.Succeeded;
                        }
                    }
                    else
                    {
                        if (User.Email.ToLower() != obj.EMAIL.ToLower() || User.UsuarioInfo.TELEFONO != obj.TELEFONO || User.UsuarioInfo.ACTIVO != obj.ACTIVO)
                        {
                            User.UserName = obj.EMAIL;
                            User.Email = obj.EMAIL;
                            User.PhoneNumber = obj.CELULAR;

                            User.UsuarioInfo.NOMBREUSUARIO = obj.NOMBREUSUARIO;
                            User.UsuarioInfo.EMAIL = obj.EMAIL;
                            User.UsuarioInfo.ACTIVO = obj.ACTIVO;
                            User.UsuarioInfo.TELEFONO = obj.TELEFONO;
                            User.UsuarioInfo.CELULAR = obj.CELULAR;

                            response.Exitoso = true;

                        }

                        var ltRole = await UserManager.GetRolesAsync(User.Id);

                        if (!ltRole.Contains(obj.ROLE))
                        {
                            foreach(var row in ltRole)
                            {
                                UserManager.RemoveFromRole(User.Id, row);
                            }
                            
                            var resu = await UserManager.AddToRoleAsync(User.Id, obj.ROLE);
                        }
                    }
                }
                catch (Exception ex)
                {
                    var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                    response.Exitoso = false;
                    response.Mensaje = "Error con codigo: " + exception;
                    throw new Exception(exception);
                }
            }
            return Ok(response);
        }

    }
}
