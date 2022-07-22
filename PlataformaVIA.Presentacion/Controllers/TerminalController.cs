namespace PlataformaVIA.Presentacion.Controllers
{
    using Core.Domain;
    using Core.Domain.Media;
    using DataTables.Mvc;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using PlataformaVIA.Core.Domain.AdministradorDocumentos;
    using PlataformaVIA.Core.Domain.Busqueda;
    using PlataformaVIA.Core.Domain.Seguridad;
    using PlataformaVIA.Presentacion.Filters;
    using PlataformaVIA.Presentacion.Models;
    using Presentacion.Helpers;
    using Services.Interfaces;
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    [Authorize]
    [PasswordExpiredAuthorize]
    [NoCache]
    [SecurityLog]
    public class TerminalController : Controller
    {
        private ITerminalService _terminalService;
        private IAdministradorDocumentosService _documentosService;

        public TerminalController()
        {
        }

        public TerminalController(ITerminalService terminalService, IAdministradorDocumentosService documentosService)
        {
            this._terminalService = terminalService;
            this._documentosService = documentosService;
        }

        // GET: Terminal
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int codTipoTerminal)
        {
            ViewBag.CodTipoTerminal = codTipoTerminal;
            return View();
        }

        public ActionResult ConsultarInstructivosPorTipo([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, int codTipoInstructivo)
        {
            try
            {
                var valorfiltro = requestModel.Search.Value.Trim();
                ResponseEO<Instructivos> resultado = new ResponseEO<Instructivos>
                {
                    IdUsuario = CrossController.Instance.GetUserInfoId(),
                    NumeroPagina = requestModel.Start / requestModel.Length,
                    TamanoPagina = requestModel.Length,
                    TextoBusqueda = valorfiltro
                };
                resultado.FiltrosCriterio = new Core.Domain.Busqueda.CriterioBusqueda { IdPadre = codTipoInstructivo };
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

                resultado = _terminalService.GetInstructivos(resultado);

                return Json(new DataTablesResponse
                (requestModel.Draw, resultado.Entidades, resultado.TotalRegistros, resultado.TotalRegistros),
                            JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        public ActionResult _GenericCarousel(int codInstructivo)
        {
            try
            {
                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

                ResponseEO<ImagenesInstructivo> response = new ResponseEO<ImagenesInstructivo>();
                CriterioBusqueda Request = new CriterioBusqueda { IdPadre = codInstructivo, IdUsuario = CrossController.Instance.GetUserInfoId(), Filtro = "", Paginacion = new ParametroPaginacion { NumeroPagina = 1, TamanoPagina = 10 } };
                response = _documentosService.GetImagenesInstructivo(Request);

                foreach(var item in response.Entidades)
                {
                    item.Ubicacion = Cipher.EncryptString(item.Ubicacion, user.Id, true);
                }
           
                return PartialView(response.Entidades);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        public ActionResult GetInstructivosProducto(int codProducto, int codTerminal)
        {
            try
            {
                ResponseEO<Instructivos> response = new ResponseEO<Instructivos>();
                response.IdUsuario = CrossController.Instance.GetUserInfoId();
                response.FiltrosCriterio = new CriterioBusqueda { IdPadre = codTerminal, IdUsuario = codProducto };

                response = _terminalService.GetInstructivosxProducto(response);

                return Json(response.Entidades, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        public ActionResult _CarouselByCategoriaTerminal(int codCategoria, int codTerminal)
        {
            try
            {
                ResponseEO<Articulo> articulos = new ResponseEO<Articulo>();
                articulos.FiltrosCriterio = new Core.Domain.Busqueda.CriterioBusqueda { IdPadre = codCategoria, IdUsuario = codTerminal };
                articulos.IdUsuario = CrossController.Instance.GetUserInfoId();
                articulos = _terminalService.GetArticulosByCategoriayTerminal(articulos);
                return PartialView("_GenericCarousel", articulos.Entidades);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }
    }
}