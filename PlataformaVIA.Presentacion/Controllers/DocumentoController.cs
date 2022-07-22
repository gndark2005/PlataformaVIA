namespace PlataformaVIA.Presentacion.Controllers
{
    using DataTables.Mvc;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using PlataformaVIA.Core.Domain;
    using PlataformaVIA.Core.Domain.Documentos;
    using PlataformaVIA.Core.Domain.Seguridad;
    using PlataformaVIA.Presentacion.Filters;
    using PlataformaVIA.Presentacion.Helpers;
    using PlataformaVIA.Presentacion.Models;
    using PlataformaVIA.Services.Interfaces;
    using System;
    using System.Web;
    using System.Web.Mvc;

    [Authorize]
    [PasswordExpiredAuthorize]
    [NoCache]
    [SecurityLog]
    public class DocumentoController : Controller
    {
        private IDocumentosService _documentosService;

        #region Constructores

        public DocumentoController(IDocumentosService documentosService)
        {
            this._documentosService = documentosService;
        }
        #endregion

        // GET: Documento
        [Authorize(Roles = "Admin,Funcionario")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ConsultarDocumentos([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            try
            {
                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

                var valorfiltro = requestModel.Search.Value.Trim();

                Core.Domain.ResponseEO<DocumentoPDF> resultado = new ResponseEO<DocumentoPDF>
                {
                    IdUsuario = CrossController.Instance.GetUserInfoId(),
                    NumeroPagina = requestModel.Start / requestModel.Length,
                    TamanoPagina = requestModel.Length,
                    TextoBusqueda = valorfiltro
                };
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

                resultado = _documentosService.GetDocumentosPDF(resultado);

                foreach (var item in resultado.Entidades)
                {
                    item.Ubicacion = (Cipher.EncryptString(item.Ubicacion, user.Id, true));
                }

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

        public virtual ActionResult DescargarDocumento(string fileName)
        {
            try
            {
                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

                string decrypt = Cipher.DecryptString(fileName, user.Id, true);
                AzureStorage.Instance.DownloadFileFromStorage(decrypt);               
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                RedirectToAction("ErrorPage", "Account", exception);
                return Json(exception.Mensaje, JsonRequestBehavior.AllowGet);
            }
        }
    }
}