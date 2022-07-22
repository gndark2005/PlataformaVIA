namespace PlataformaVIA.Presentacion.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using PlataformaVIA.Core.Domain;
    using PlataformaVIA.Core.Domain.AdministradorDocumentos;
    using PlataformaVIA.Core.Domain.Seguridad;
    using PlataformaVIA.Data.Repositories.Implementations;
    using PlataformaVIA.Data.Repositories.Interfaces;
    using PlataformaVIA.Presentacion.Filters;
    using PlataformaVIA.Presentacion.Helpers;
    using PlataformaVIA.Presentacion.Models;
    using PlataformaVIA.Services.Implementations;
    using PlataformaVIA.Services.Interfaces;

    [NoCache]
    [SecurityLog]
    public class HomeController : Controller
    {
        private IViaBalotoService ViaBalotoService;
        private IViaBalotoRepository ViaBalotoRepository;
        private IAdministradorDocumentosService InstructivoService;
        private IAdministradorDocumentosRepository InstructivoRepository;
        private IDivulgacionService DivulgacionService;
        private IDivulgacionRepository DivulgacionRepository;
        private IAdministracionCorreoService AdministracionCorreoService;
        private IAdministracionCorreoRepository AdministracionCorreoRepository;

        #region "Constructores"
        public HomeController()
        {
            this.ViaBalotoRepository = new ViaBalotoRepository();
            this.ViaBalotoService = new ViaBalotoService(this.ViaBalotoRepository);

            this.InstructivoRepository = new AdministradorDocumentosRepository();
            this.InstructivoService = new AdministradorDocumentosService(this.InstructivoRepository);

            this.DivulgacionRepository = new DivulgacionRepository();
            this.DivulgacionService = new DivulgacionService(this.DivulgacionRepository);

            this.AdministracionCorreoRepository = new AdministracionCorreoRepository();
            this.AdministracionCorreoService = new AdministracionCorreoService(this.AdministracionCorreoRepository);
        }
        #endregion

        [Authorize]
        public ActionResult Index()
        {
            List<Divulgacion> pendiente = new List<Divulgacion>();

            var aa = CrossController.Instance.GetUserInfoId();

            pendiente = DivulgacionRepository.ObtenerDivulgacionxUsuario(CrossController.Instance.GetUserInfoId()).ToList();

            if (pendiente.Count > 0)
            {
                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
                
                var excepciones = DivulgacionRepository.ValidarNitxDivulgacion(new ExcepcionxNIT { CodDivulgacion = Int32.Parse(pendiente.First().ID_DIVULGACION.ToString()), NIT = user.UsuarioInfo.RazonSocial.NIT.ToString() });

                if (excepciones == null || excepciones.ToList().Count <= 0)
                {
                    Session["CurrentDivulgacion"] = pendiente.First();
                    return RedirectToAction("AceptarDivulgacion");
                }               
            }
            
            //var email = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId()).Email;

            //TODO Consultar Web API para traer datos por email

            if (User.IsInRole("Funcionario") || User.IsInRole("FuncionarioExterno"))
            {
                return RedirectToAction("Index", "Funcionario");
            }

            if (User.IsInRole("Representante Legal"))
            {
                return RedirectToAction("Index", "RepresentanteLegal");
            }
            //role Vendedor
            if (User.IsInRole("Punto de Venta"))
            {
                return RedirectToAction("Index", "PuntoDeVenta");
            }
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("IndexCarousel", "AdministradorDocumentos");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }                                   
        }

        [Authorize]
        public ActionResult AceptarDivulgacion()
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            Divulgacion model = (Divulgacion)Session["CurrentDivulgacion"];

            if(user != null)
            {
                model.RAZONSOCIAL = user.UsuarioInfo.RazonSocial.NOMBRERAZONSOCIAL;
                model.NIT = user.UsuarioInfo.RazonSocial.NIT.ToString();
            }
            
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult AceptarDivulgacion(Divulgacion model)
        {
            try {
                model.FECHAHORAMODIFICACION = DateTime.Now;
                model.CODUSUARIOINFOMODIFICACION = CrossController.Instance.GetUserInfoId();

                string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(ip))
                {
                    ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                model.DIRECCIONIP = Request.UserHostAddress;

                model.BROWSERID = Request.Browser.Id + " " + Request.Browser.Version;

                model.COMPUTERID = Request.UserHostName;

                string acepta = "";

                if (model.ACEPTADO)
                {
                    acepta = "aceptado";
                }
                else
                {
                    acepta = "rechazado";
                }

                //AGREGAR FECHA INICIO Y FECHA FIN AL METODO DEL REPORTE

                int insertado = DivulgacionRepository.InsertarRespuestaDivulgacion(model);

                if(model.CONTAINERPATH != null && model.CONTAINERPATH != "")
                {
                    ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

                    string file = model.CONTAINERPATH + user.UsuarioInfo.RazonSocial.NIT + "CambioContratoColaboracion.pdf";
                    string url = string.Format("{0}://{1}", Request.RequestContext.HttpContext.Request.Url.Scheme, Request.RequestContext.HttpContext.Request.Url.Authority);
                    
                    UrlHelper Url = new UrlHelper(Request.RequestContext);

                    var callbackUrl = Url.Content("~/Certificado/ObtenerDocumento/" + "TOKEN_ID");

                    int index = callbackUrl.IndexOf("Certificado");

                    string UrlFinal = url + "/" + callbackUrl.Substring(index);

                    string cuerpoMsj = " <br /><a href =\"" + UrlFinal + "\">Descargar Archivo.</a>";

                    AdministracionCorreo correo = new AdministracionCorreo { REQUIERE_TOKEN = 1, PATH_STORAGE = file, ASUNTO = "Divulgación " + model.NOMBRE, DESTINATARIOS = CrossController.Instance.GetUserInfoEmail(), COPIA_DESTINATARIOS = "", MENSAJE = "<span>Estimado usuario, usted ha " + acepta + " nuestra divulgación " + model.TITULO + " en la fecha: " + DateTime.Now + " </br> " + cuerpoMsj + "</span>", TITULO = "DIVULGACIÓN " + model.TITULO };

                    AdministracionCorreoRepository.AddCorreoPendiente(correo);

                    
                }
                else
                {
                    string cuerpoMsj = HttpUtility.UrlDecode(model.TEXTO);

                    AdministracionCorreo correo = new AdministracionCorreo { ASUNTO = "Divulgación " + model.NOMBRE, DESTINATARIOS = CrossController.Instance.GetUserInfoEmail(), COPIA_DESTINATARIOS = "", MENSAJE = "<span>Estimado usuario, usted ha " + acepta + " nuestra divulgación " + model.TITULO + " en la fecha: " + DateTime.Now + " </br> " + cuerpoMsj + "</span>", TITULO = "DIVULGACIÓN " + model.TITULO };
                    ResponseIndividualEO<AdministracionCorreo> correoNuevo = new ResponseIndividualEO<AdministracionCorreo>();
                    correoNuevo.Entidad = correo;
                    AdministracionCorreoRepository.SendEmail(correoNuevo);
                }

                

                if (!model.ACEPTADO)
                {
                    if (User.IsInRole("Funcionario") || User.IsInRole("FuncionarioExterno"))
                    {
                        return RedirectToAction("Index", "Funcionario");
                    }

                    if (User.IsInRole("Representante Legal"))
                    {
                        return RedirectToAction("Index", "RepresentanteLegal");
                    }
                    //role Vendedor
                    if (User.IsInRole("Punto de Venta"))
                    {
                        return RedirectToAction("Index", "PuntoDeVenta");
                    }
                    if (User.IsInRole("Admin"))
                    {
                        return RedirectToAction("IndexCarousel", "AdministradorDocumentos");
                    }
                }


                return RedirectToAction("Index");
            } catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }


        }



        public ActionResult About()
        {
            ViewBag.Title = "CONTÁCTANOS";
            ViewBag.Message = "Tus inquietudes y comentarios son importantes para nosotros.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]
        public ActionResult _Carousel()
        {
            int tipoInstructivo = (int)TipoDocumentoEnum.Banner;

            if (Request.Browser.IsMobileDevice)
            {
                tipoInstructivo = (int)TipoDocumentoEnum.BannerResponsive;
            }

            try
            {
                Sorteo articulos = new Sorteo();

                var Ltfiles = InstructivoService.GetAllInstructivoXTipo(tipoInstructivo);

                articulos = ViaBalotoService.ConsultarDatosSorteo();
                articulos.Banners = Ltfiles;

                return PartialView(articulos);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        [Authorize]
        public ActionResult GetImagenBackground(string ubicacion)
        {
            try
            {
                FileConstructor file = AzureStorage.Instance.GetFileFromStorage(ubicacion);
                return File(file.ByteArray, file.TipoArchivo, file.NombreArchivo);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

    }
}