using DataTables.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PlataformaVIA.Core.Domain;
using PlataformaVIA.Core.Domain.AdministradorDocumentos;
using PlataformaVIA.Core.Domain.Busqueda;
using PlataformaVIA.Core.Domain.RepresentanteLegal;
using PlataformaVIA.Core.Domain.Seguridad;
using PlataformaVIA.Presentacion.Filters;
using PlataformaVIA.Presentacion.Helpers;
using PlataformaVIA.Presentacion.Models;
using PlataformaVIA.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlataformaVIA.Presentacion.Controllers
{
    [Authorize]
    [PasswordExpiredAuthorize]
    [NoCache]
    [SecurityLog]
    public class AdministradorDocumentosController : Controller
    {

        private IAdministradorDocumentosService _administradorDocumentosService;

        #region Constructores
        public AdministradorDocumentosController(IAdministradorDocumentosService administradorDocumentosService)
        {
            this._administradorDocumentosService = administradorDocumentosService;
        }
        #endregion

        // GET: AdministradorDocumentos
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexCarousel()
        {
            return View();
        }
       
        [Authorize(Roles = "Admin")]
        public ActionResult _CrearDocumento()
        {
            try
            {
                List<TipoTerminal> terminales = _administradorDocumentosService.GetTiposTerminal().ToList();
                terminales.Insert(0, new TipoTerminal { IdTerminal = 0, NombreTerminal = "Seleccione..." });
                ViewBag.Terminales = terminales.Select(a => new SelectListItem { Text = a.NombreTerminal, Value = a.IdTerminal.ToString() });

                List<TiposDocumento> documentos = _administradorDocumentosService.GetTiposDocumento().ToList();
                documentos.Insert(0, new TiposDocumento { IdInstructivo = 0, NombreInstructivo = "Seleccione..." });
                ViewBag.Documentos = documentos.Select(a => new SelectListItem { Text = a.NombreInstructivo, Value = a.IdInstructivo.ToString() });

                List<Categorias> categorias = _administradorDocumentosService.GetCategorias().ToList();
                categorias.Insert(0, new Categorias { IdCategoria = 0, NombreCategoria = "Seleccione..." });
                ViewBag.Categorias = categorias.Select(a => new SelectListItem { Text = a.NombreCategoria, Value = a.IdCategoria.ToString() });

                var tipoImagen = Enum.GetValues(typeof(TipoImagenEnum)).Cast<TipoImagenEnum>().ToList();
                ViewBag.TipoImagen = tipoImagen.Select(a => new SelectListItem { Text = a.ToString(), Value = a.ToString() });

                Instructivos instructivo = new Instructivos();

                return PartialView(instructivo);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async System.Threading.Tasks.Task<ActionResult> _CrearDocumento(Instructivos model)
        {
            if (ModelState.IsValid)
            {
                int idUser = CrossController.Instance.GetUserInfoId();
                IEnumerable<TiposDocumento> documentos = _administradorDocumentosService.GetTiposDocumento();
                string ubicacion = documentos.FirstOrDefault(e => e.IdInstructivo == model.CodTipoInstructivo).NombreInstructivo + "/";
                byte count = 0;
                try
                {
                    string[] urlList = model.URL.Split(',');

                    int idInstructivo = _administradorDocumentosService.CrearInstructivo(model, idUser);

                    foreach (var item in model.Files)
                    {
                       
                        if (item != null)
                        {
                            string guid = Guid.NewGuid().ToString();
                            string filename = guid + item.FileName;

                            bool isInserted = await AzureStorage.Instance.InsertIntoStorage(item, ubicacion, filename);

                            if (isInserted)
                            {

                                InstructivoUbicacion instructivoParam = new InstructivoUbicacion();
                                instructivoParam.CodInstructivoTerminal = idInstructivo;
                                instructivoParam.Orden = count;
                                instructivoParam.NombreArchivo = item.FileName;
                                instructivoParam.Ubicacion = ubicacion + filename;
                                instructivoParam.URL = urlList[count + 1].Trim();
                                if(model.TipoImagen != null)
                                {
                                    instructivoParam.TipoImagen = model.TipoImagen[count].ToString().Trim();
                                }
                                else
                                {
                                    instructivoParam.TipoImagen = Enum.GetName(typeof(TipoImagenEnum), 1);
                                }
                                
                                int created = _administradorDocumentosService.CrearUbicacionInstructivo(instructivoParam, idUser);
                                count++;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                    return RedirectToAction("ErrorPage", "Account", exception);
                }
                ViewBag.InstructivoMessage = "Creado";
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Error");
            }
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async System.Threading.Tasks.Task<ActionResult> _ModificarDocumento(Instructivos model)
        {

            int idUser = CrossController.Instance.GetUserInfoId();
            try
            {
                bool isModified = _administradorDocumentosService.ModificarInstructivo(model, idUser);
                if (isModified)
                {
                    ViewBag.InstructivoMessage = "Modificado";
                }
                else
                {
                    ViewBag.InstructivoMessage = "Error modificando documento";
                }
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult GetFiltroBasicoDocumentos([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            try
            {
                // Aplicamos filtros de búsqueda
                var valorfiltro = requestModel.Search.Value.Trim();

                #region Ordenado
                // Sorting
                var sortedColumns = requestModel.Columns.GetSortedColumns();
                var orderByString = String.Empty;

                foreach (var column in sortedColumns)
                {
                    orderByString += orderByString != String.Empty ? "," : "";
                    orderByString += (column.Data) +
                      (column.SortDirection ==
                      Column.OrderDirection.Ascendant ? " asc" : " desc");
                }


                #endregion Ordenado

                // Paginación           
                ResponseEO<Instructivos> response = new ResponseEO<Instructivos>();
                response.IdUsuario = CrossController.Instance.GetUserInfoId();
                response.FiltrosCriterio = new CriterioBusqueda { Filtro = valorfiltro, Paginacion = new ParametroPaginacion { NumeroPagina = requestModel.Start / requestModel.Length, TamanoPagina = requestModel.Length } };

                response = _administradorDocumentosService.GetInstructivos(response);

                return Json(new DataTablesResponse
                (requestModel.Draw, response.Entidades, response.FiltrosCriterio.Paginacion.TotalRegistros, response.FiltrosCriterio.Paginacion.TotalRegistros),
                            JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult _DetalleDocumento(int id)
        {
            try
            {
                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

                ResponseIndividualEO<Instructivos> response = new ResponseIndividualEO<Instructivos>();
                CriterioBusqueda Request = new CriterioBusqueda();
                Request.IdPadre = id;
                Request.IdUsuario = CrossController.Instance.GetUserInfoId();
                response = _administradorDocumentosService.GetDetallesInstructivo(Request);

                //response.Entidad.Ubicacion 

                return PartialView(response.Entidad);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult _ModificarDocumento(int id)
        {
            try
            {


                IEnumerable<TipoTerminal> terminales = _administradorDocumentosService.GetTiposTerminal();
                ViewBag.Terminales = terminales.Select(a => new SelectListItem { Text = a.NombreTerminal, Value = a.IdTerminal.ToString() });

                IEnumerable<TiposDocumento> documentos = _administradorDocumentosService.GetTiposDocumento();
                ViewBag.Documentos = documentos.Select(a => new SelectListItem { Text = a.NombreInstructivo, Value = a.IdInstructivo.ToString() });

                IEnumerable<Categorias> categorias = _administradorDocumentosService.GetCategorias();
                ViewBag.Categorias = categorias.Select(a => new SelectListItem { Text = a.NombreCategoria, Value = a.IdCategoria.ToString() });

                var tipoImagen = Enum.GetValues(typeof(TipoImagenEnum)).Cast<TipoImagenEnum>().ToList();
                ViewBag.TipoImagen = tipoImagen.Select(a => new SelectListItem { Text = a.ToString(), Value = a.ToString() });

                ResponseIndividualEO<Instructivos> response = new ResponseIndividualEO<Instructivos>();
                CriterioBusqueda Request = new CriterioBusqueda();
                Request.IdPadre = id;
                Request.IdUsuario = CrossController.Instance.GetUserInfoId();
                response = _administradorDocumentosService.GetDetallesInstructivo(Request);
                return PartialView(response.Entidad);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        [Authorize(Roles = "Admin")]
        public async System.Threading.Tasks.Task<ActionResult> _EliminarDocumento(int id)
        {
            int idUser = CrossController.Instance.GetUserInfoId();
            try
            {
                CriterioBusqueda Request = new CriterioBusqueda { IdPadre = id, IdUsuario = CrossController.Instance.GetUserInfoId(), Filtro = "", Paginacion = new ParametroPaginacion { NumeroPagina = 1, TamanoPagina = 10 } };
                Request.IdPadre = id;
                Request.IdUsuario = CrossController.Instance.GetUserInfoId();

                var response = _administradorDocumentosService.GetImagenesInstructivo(Request);

                foreach (var item in response.Entidades)
                {
                    //item.Ubicacion
                    //todo eliminar del azure
                }

                Instructivos model = new Instructivos { IdInstructivo = id };

                bool isDeleted = _administradorDocumentosService.EliminarInstructivo(model, idUser);
                if (isDeleted)
                {
                    return Json("ok", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("Error", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }


        public ActionResult GetImagenesDetalle([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, int idDoc)
        {
            try
            {
                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

                // Aplicamos filtros de búsqueda
                var valorfiltro = requestModel.Search.Value.Trim();

                #region Ordenado
                // Sorting
                var sortedColumns = requestModel.Columns.GetSortedColumns();
                var orderByString = String.Empty;

                foreach (var column in sortedColumns)
                {
                    orderByString += orderByString != String.Empty ? "," : "";
                    orderByString += (column.Data) +
                      (column.SortDirection ==
                      Column.OrderDirection.Ascendant ? " asc" : " desc");
                }

                #endregion Ordenado

                ResponseEO<ImagenesInstructivo> response = new ResponseEO<ImagenesInstructivo>();
                CriterioBusqueda Request = new CriterioBusqueda { IdPadre = idDoc, IdUsuario = CrossController.Instance.GetUserInfoId(), Filtro = valorfiltro, Paginacion = new ParametroPaginacion { NumeroPagina = requestModel.Start, TamanoPagina = requestModel.Length } };
                Request.IdPadre = idDoc;
                Request.IdUsuario = CrossController.Instance.GetUserInfoId();
                
                response = _administradorDocumentosService.GetImagenesInstructivo(Request);

                foreach (var item in response.Entidades)
                {
                    item.Ubicacion = Cipher.EncryptString(item.Ubicacion, user.Id, true);
                }

                    return Json(new DataTablesResponse
                 (requestModel.Draw, response.Entidades, Request.Paginacion.TotalRegistros, Request.Paginacion.TotalRegistros),
                             JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        public ActionResult MostrarArchivo(string ubicacion)
        {
            try
            {
                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

                string decrypt = Cipher.DecryptString(ubicacion, user.Id, true);
                FileConstructor file = AzureStorage.Instance.GetFileFromStorage(decrypt);
                return File(file.ByteArray, file.TipoArchivo);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        public ActionResult MostrarDocumento(string ubicacion)
        {
            try
            {
                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

                string decrypt = Cipher.DecryptString(ubicacion, user.Id, true);
                FileConstructor file = AzureStorage.Instance.GetFileFromStorage(decrypt);
                return File(file.ByteArray, file.TipoArchivo);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async System.Threading.Tasks.Task<JsonResult> ModificarImagen(FormCollection formCollection)
        {
            try
            {
                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());


                int documentId = int.Parse(formCollection["idDoc"]);
                string ubicacion = formCollection["ubicacion"];

                string decrypt = Cipher.DecryptString(ubicacion, user.Id, true);

                string[] folder = decrypt.Split('/');
                
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];

                    bool deleted = AzureStorage.Instance.DeleteFileStorage(ubicacion);
                    if (deleted)
                    {
                        string guid = Guid.NewGuid().ToString();
                        string filename = guid + file.FileName;
                        bool isCreated = await AzureStorage.Instance.InsertIntoStorage(file, folder[0] + "/", filename);

                        if (isCreated)
                        {
                            ImagenesInstructivo imagen = new ImagenesInstructivo { IdInstructivoTipoTerminal = documentId, Ubicacion = folder[0] + "/" + filename, NombreArchivo = file.FileName };
                            bool updated = _administradorDocumentosService.ModificarInstructivoImagen(imagen, CrossController.Instance.GetUserInfoId());

                            if (updated)
                            {
                                return Json("ok");
                            }
                        }
                    }
                    else
                    {
                        return Json("Error");
                    }
                }
                
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                RedirectToAction("ErrorPage", "Account", exception);
            }

            return Json("Error");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public JsonResult ModificarAtributoImagen(int idDoc, string value)
        {
            try
            {

                ImagenesInstructivo imagen = new ImagenesInstructivo { IdInstructivoTipoTerminal = idDoc, Atributo = value };
                bool updated = _administradorDocumentosService.ModificarAtributoImagen(imagen);

                if (updated)
                {
                    return Json("ok");
                }

            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                RedirectToAction("ErrorPage", "Account", exception);
            }

            return Json("Error");
        }

        public JsonResult ModificarImagenURL(int idDoc, string value)
        {
            try
            {

                ImagenesInstructivo imagen = new ImagenesInstructivo { IdInstructivoTipoTerminal = idDoc, UrlImagen = value };
                bool updated = _administradorDocumentosService.ModificarImagenURL(imagen);

                if (updated)
                {
                    return Json("ok");
                }

            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                RedirectToAction("ErrorPage", "Account", exception);
            }

            return Json("Error");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async System.Threading.Tasks.Task<JsonResult> AgregarImagen(FormCollection formCollection)
        {
            try
            {
                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

                string tipo = formCollection["tipo"];
                string url = formCollection["url"];

                int codInstructivoTerminal = int.Parse(formCollection["codInstructivoTerminal"]);
                string ubicacion = formCollection["ubicacion"];

                string decrypt = Cipher.DecryptString(ubicacion, user.Id, true);

                string[] folder = decrypt.Split('/');

                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];

                    string guid = Guid.NewGuid().ToString();
                    string filename = guid + file.FileName;

                    bool added = await AzureStorage.Instance.InsertIntoStorage(file, folder[0] + "/", filename);
                    if (added)
                    {

                        ImagenesInstructivo imagen = new ImagenesInstructivo { IdInstructivoTipoTerminal = codInstructivoTerminal, Ubicacion = folder[0] + "/" + filename, NombreArchivo = file.FileName, Atributo = tipo, UrlImagen = url };
                        bool updated = _administradorDocumentosService.AgregarInstructivoImagen(imagen, CrossController.Instance.GetUserInfoId());

                        if (updated)
                        {
                            return Json("ok");
                        }

                    }
                    else
                    {
                        return Json("Error");
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                RedirectToAction("ErrorPage", "Account", exception);
            }

            return Json("Error");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async System.Threading.Tasks.Task<JsonResult> EliminarImagen(FormCollection formCollection)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            int documentId = int.Parse(formCollection["idDoc"]);
            string ubicacion = formCollection["ubicacion"];

            string decrypt = Cipher.DecryptString(ubicacion, user.Id, true);

            string[] folder = decrypt.Split('/');

            try
            {
               
                bool deleted = AzureStorage.Instance.DeleteFileStorage(decrypt);
                if (deleted)
                {

                    bool updated = _administradorDocumentosService.EliminarImagenInstructivo(new ImagenesInstructivo { IdInstructivoTipoTerminal = documentId }, CrossController.Instance.GetUserInfoId());

                    if (updated)
                    {
                        return Json("ok");
                    }
                }
                else
                {
                    return Json("Error");
                }

            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                RedirectToAction("ErrorPage", "Account", exception);
            }

            return Json("ok");

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async System.Threading.Tasks.Task<JsonResult> ModificarOrden(FormCollection formCollection)
        {
            try
            {
                int documentId = int.Parse(formCollection["idDoc"]);
                byte orden = byte.Parse(formCollection["orden"]);
                int accion = int.Parse(formCollection["accion"]);

                bool updated = _administradorDocumentosService.CambiarOrdenImagen(new ImagenesInstructivo { IdInstructivoTipoTerminal = documentId, Orden = orden },
                    CrossController.Instance.GetUserInfoId(), accion);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                RedirectToAction("ErrorPage", "Account", exception);
            }
            return Json("ok");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async System.Threading.Tasks.Task<JsonResult> MostrarOpcionesCreacion(int idDoc)
        {
            try
            {
                IEnumerable<TiposDocumento> documentos = _administradorDocumentosService.GetTiposDocumento();
                TiposDocumento docBusqueda = documentos.FirstOrDefault(c => c.IdInstructivo.Equals(idDoc));

                if (docBusqueda.DiferenciaTerminal == true)
                {
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }

            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                RedirectToAction("ErrorPage", "Account", exception);
            }

            return Json("Error");

        }
    }
}