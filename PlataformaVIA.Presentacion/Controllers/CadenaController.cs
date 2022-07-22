namespace PlataformaVIA.Presentacion.Controllers
{
    using Core.Domain;
    using Core.Domain.Busqueda;
    using Core.Domain.Cadena;
    using DataTables.Mvc;
    using ViewModels.BusquedaAvanzada;
    using Helpers;
    using Services.Interfaces;
    using System;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using PlataformaVIA.Presentacion.Models;
    using System.Web;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using PlataformaVIA.Core.Domain.Seguridad;
    using PlataformaVIA.Core.Domain.Parametro;
    using PlataformaVIA.Core.Domain.Reportes;
    using System.Collections.Generic;
    using PlataformaVIA.Core.Domain.PuntoDeVenta;
    using PlataformaVIA.Presentacion.Filters;

    [Authorize]
    [PasswordExpiredAuthorize]
    [NoCache]
    [SecurityLog]
    public class CadenaController : Controller
    {
        private ICadenaService _cadenaService;
        private ICicloFacturacionService _ciclofacturacionService;
        private IParametroService _parametroService;
        private IPuntoVentaService _puntoventaService;

        #region Constructores

        public CadenaController(ICadenaService cadenaService, ICicloFacturacionService cicloFacturacion, IParametroService parametroService,  IPuntoVentaService puntoVentaService)
        {
            this._cadenaService = cadenaService;
            this._ciclofacturacionService = cicloFacturacion;
            this._parametroService = parametroService;
            this._puntoventaService = puntoVentaService;
        }
        #endregion


        //// GET: Cadena
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public async Task<ActionResult> ConsultarCadenas()
        {
            return View();
        }

        public async Task<ActionResult> _ConsultarDetalleCarteraPorPDV(Cadena cadena)
        {
            return PartialView(cadena);
        }

        public ActionResult Details(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        public async Task<ActionResult> _ConsultarPagos(Cadena cadena)
        {
            return PartialView(cadena);
        }

        public async Task<ActionResult> _ConsultarAjustes(Cadena cadena)
        {
            return PartialView(cadena);
        }

        public ActionResult _EstadoDeCuentaCadenaPorLineaDeNegocio(Cadena cadena)
        {
            return View(cadena);
        }

        [HttpGet]
        public ActionResult BusquedaAvanzadaMisPagos()
        {
            var busquedaavanzadaVM = new BusquedaAvanzadaCicloFacturacionViewModel
            {
                CicloFacturacionList = new SelectList(_ciclofacturacionService.GetCicloFacturacion(false),
                                                                        "Id",
                                                                        "RangoFechaDepositos")
            };
            return View("_BusquedaAvanzadaMisPagos", busquedaavanzadaVM);
        }

        [HttpGet]
        public ActionResult BusquedaAvanzadaMisAjustes()
        {
            var busquedaavanzadaVM = new BusquedaAvanzadaCicloFacturacionViewModel
            {
                CicloFacturacionList = new SelectList(_ciclofacturacionService.GetCicloFacturacion(false),
                                                                        "Id",
                                                                        "RangoFechaDepositos")
            };
            return View("_BusquedaAvanzadaMisAjustes", busquedaavanzadaVM);
        }

        [HttpPost]
        public ActionResult GetFiltroAvanzadoMisPagos([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, BusquedaAvanzadaCicloFacturacionViewModel searchViewModel)
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

            var criterioBusqueda = new CriterioBusquedaCicloFacturacion { CodCicloFacturacion = searchViewModel.CodCicloFacturacion, IdPadre = searchViewModel.CodPuntoVenta, Filtro = valorfiltro, Paginacion = new ParametroPaginacion { NumeroPagina = requestModel.Start / requestModel.Length, TamanoPagina = requestModel.Length } };
            var response = new ResponseEO<Pago> { FiltrosCicloFacturacion = criterioBusqueda, NumeroPagina = requestModel.Start / requestModel.Length, TamanoPagina = requestModel.Length };
            response.IdUsuario = CrossController.Instance.GetUserInfoId();
            //TODO cambiar a repositorio de cadena cuando esté el sp
            response = _cadenaService.GetMisPagos(response);
            var lstcadenas = response.Entidades;

            var jsonResult = Json(new DataTablesResponse
            (requestModel.Draw, lstcadenas, response.TotalRegistros, response.TotalRegistros), JsonRequestBehavior.DenyGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public ActionResult GetFiltroAvanzadoMisAjustes([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, BusquedaAvanzadaCicloFacturacionViewModel searchViewModel)
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

            var criterioBusqueda = new CriterioBusquedaCicloFacturacion { CodCicloFacturacion = searchViewModel.CodCicloFacturacion, IdPadre = searchViewModel.CodPuntoVenta, Filtro = valorfiltro, Paginacion = new ParametroPaginacion { NumeroPagina = requestModel.Start / requestModel.Length, TamanoPagina = requestModel.Length } };
            var response = new ResponseEO<Ajuste> { FiltrosCicloFacturacion = criterioBusqueda, NumeroPagina = requestModel.Start, TamanoPagina = requestModel.Length };
            response.IdUsuario = CrossController.Instance.GetUserInfoId();

            //TODO ajustar al sp correcto
            response = _cadenaService.GetMisAjustes(response);
            var lstcadenas = response.Entidades;

            return Json(new DataTablesResponse
            (requestModel.Draw, lstcadenas, response.TotalRegistros, response.TotalRegistros),
                        JsonRequestBehavior.DenyGet);
        }

        public ActionResult GetFiltroBasicoCadenas([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            try
            {
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

                //query = query.OrderBy(orderByString ==
                //string.Empty ? "BarCode asc" : orderByString);

                #endregion Ordenado

                // Paginación
                // query = query.Skip(requestModel.Start).Take(requestModel.Length);

                var response = new ResponseEO<Cadena>();
                response.IdUsuario = CrossController.Instance.GetUserInfoId();
                response.FiltrosCriterio = new CriterioBusqueda { Filtro = valorfiltro, Paginacion = new ParametroPaginacion { NumeroPagina = requestModel.Start / requestModel.Length, TamanoPagina = requestModel.Length } };
                var lstcadenas = _cadenaService.GetCadenaXRazonSocial(response);


                var jsonResult = Json(new DataTablesResponse
                (requestModel.Draw, response.Entidades, response.FiltrosCriterio.Paginacion.TotalRegistros, response.FiltrosCriterio.Paginacion.TotalRegistros), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        [HttpPost]
        public JsonResult GetFiltroBasicoPDVAsociados([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, int idCadena)
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

                //query = query.OrderBy(orderByString ==
                //string.Empty ? "BarCode asc" : orderByString);

                #endregion Ordenado

                // Paginación
                // query = query.Skip(requestModel.Start).Take(requestModel.Length);

                var response = new ResponseEO<EstadoPuntoVentaXLineadeNegocio>();
                response.IdUsuario = CrossController.Instance.GetUserInfoId();
                response.FiltrosCriterio = new CriterioBusqueda { IdPadre = idCadena, Filtro = valorfiltro, Paginacion = new ParametroPaginacion { NumeroPagina = requestModel.Start / requestModel.Length, TamanoPagina = requestModel.Length } };
                var lstcadenas = _cadenaService.GetEstadosPuntosDeVentaAsociados(response);

                var jsonResult = Json(new DataTablesResponse
                (requestModel.Draw, response.Entidades, response.FiltrosCriterio.Paginacion.TotalRegistros, response.FiltrosCriterio.Paginacion.TotalRegistros), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                RedirectToAction("ErrorPage", "Account", exception);
                JsonResult res = new JsonResult();
                return res;
            }
        }

        public ActionResult GetPartialView(string idlink, int idCadena)
        {
            try
            {
                ViewBag.MostrarNoficiaciones = false;
                switch (idlink)
                {
                    case "Inicio":
                        ResponseIndividualEO<Cadena> cadena = new ResponseIndividualEO<Cadena>();
                        CriterioBusqueda request = new CriterioBusqueda();
                        request.IdPadre = idCadena;
                        request.IdUsuario = CrossController.Instance.GetUserInfoId();
                        cadena = _cadenaService.GetInformacionCadena(request);
                        if (cadena.Entidad == null)
                        {
                            return HttpNotFound();
                        }
                        return PartialView("_DetalleCadena", cadena.Entidad);
                    case "Encabezado":
                        ResponseIndividualEO<Cadena> cadEncabezado = new ResponseIndividualEO<Cadena>();
                        CriterioBusqueda req = new CriterioBusqueda();
                        req.IdPadre = idCadena;
                        req.IdUsuario = CrossController.Instance.GetUserInfoId();
                        cadEncabezado = _cadenaService.GetInformacionCadena(req);
                        if (cadEncabezado.Entidad == null)
                        {
                            return HttpNotFound();
                        }
                        return Json(cadEncabezado.Entidad, JsonRequestBehavior.AllowGet);
                    case "Facturacion":
                        return PartialView("_ConsultarFacturacion", new Cadena { Id = idCadena });
                    case "DetalleCartera":
                        return PartialView("_ConsultarDetalleCarteraPorPDV", new Cadena { Id = idCadena });
                    case "DetalleCupo":
                        return PartialView("_ConsultarDetalleCupoPorPDV", new Cadena { Id = idCadena });
                    case "Pagos":
                        return PartialView("_ConsultarPagos", new Cadena { Id = idCadena });
                    case "Ajustes":
                        return PartialView("_ConsultarAjustes", new Cadena { Id = idCadena });
                    case "Reportes":
                        return PartialView("_ConsultarReportes", new Cadena { Id = idCadena });
                    default: break;
                }

                return RedirectToAction("YourHomePage");
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }


        [HttpGet]
        public ActionResult _BusquedaAvanzadaFacturacion()
        {
            var busquedaavanzadaVM = new BusquedaAvanzadaCicloFacturacionViewModel
            {
                CicloFacturacionList = new SelectList(_ciclofacturacionService.GetCicloFacturacion(false),
                                                                        "Id",
                                                                        "RangoFechaFacturacion")
            };
            return View("_BusquedaAvanzadaFacturacion", busquedaavanzadaVM);
        }

        public ActionResult GetFiltroAvanzadoFacturacion([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, BusquedaAvanzadaCicloFacturacionViewModel searchViewModel)
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

                var criterioBusqueda = new CriterioBusquedaCicloFacturacion { CodCicloFacturacion = searchViewModel.CodCicloFacturacion, IdPadre = searchViewModel.CodCadena, Filtro = valorfiltro, Paginacion = new ParametroPaginacion { NumeroPagina = requestModel.Start / requestModel.Length, TamanoPagina = requestModel.Length } };

                ResponseEO<Facturacion> response = new ResponseEO<Facturacion>();
                response.FiltrosCicloFacturacion = criterioBusqueda;
                response.IdUsuario = CrossController.Instance.GetUserInfoId();
                var lstestadoscuenta = _cadenaService.GetFacturacion(response);

                return Json(new DataTablesResponse
                (requestModel.Draw, lstestadoscuenta.Entidades, response.FiltrosCicloFacturacion.Paginacion.TotalRegistros, response.FiltrosCicloFacturacion.Paginacion.TotalRegistros),
                            JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        [HttpPost]
        public ActionResult GetFiltroAvanzadoDetalleCarteraPorPDV([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, BusquedaAvanzadaCicloFacturacionViewModel busqueda)
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


                var criterioBusqueda = new CriterioBusquedaCicloFacturacion { IdPadre = busqueda.CodCadena, Filtro = valorfiltro, Paginacion = new ParametroPaginacion { NumeroPagina = requestModel.Start / requestModel.Length, TamanoPagina = requestModel.Length } };
                var response = new ResponseEO<DetalleCarteraPuntoVenta> { IdUsuario = CrossController.Instance.GetUserInfoId(), FiltrosCicloFacturacion = criterioBusqueda, NumeroPagina = requestModel.Start, TamanoPagina = requestModel.Length };
                response = _cadenaService.GetDetalleCarteraPorPDV(response);
                var lstcadenas = response.Entidades;

                return Json(new DataTablesResponse
                (requestModel.Draw, lstcadenas, response.FiltrosCicloFacturacion.Paginacion.TotalRegistros, response.FiltrosCicloFacturacion.Paginacion.TotalRegistros),
                            JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        [HttpPost]
        public ActionResult GetFiltroAvanzadoDetalleCupoPorPDV([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, BusquedaAvanzadaCicloFacturacionViewModel busqueda)
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

            var criterioBusqueda = new CriterioBusquedaCicloFacturacion { IdPadre = busqueda.CodCadena, Filtro = valorfiltro, Paginacion = new ParametroPaginacion { NumeroPagina = requestModel.Start / requestModel.Length, TamanoPagina = requestModel.Length } };
            var response = new ResponseEO<DetalleCupoPuntodeVenta> { IdUsuario = CrossController.Instance.GetUserInfoId(), FiltrosCicloFacturacion = criterioBusqueda, NumeroPagina = requestModel.Start / requestModel.Length, TamanoPagina = requestModel.Length };
            response = _cadenaService.GetDetalleCupoPorPDV(response);
            var lstcadenas = response.Entidades;

            return Json(new DataTablesResponse
            (requestModel.Draw, lstcadenas, response.FiltrosCicloFacturacion.Paginacion.TotalRegistros, response.FiltrosCicloFacturacion.Paginacion.TotalRegistros),
                        JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> GenerarTirillaCadena(string ubicacion)
        {
            Parametro parametro = _parametroService.BuscarParametro(CrossController.Instance.GetUserInfoId(), "RutaTirillasCadena");

            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            string[] rutaCompleta = ubicacion.Trim().Split('-');

            string tirilla = parametro.VALOR + rutaCompleta[0] + "/" + rutaCompleta[1];

            bool exists = await AzureStorage.Instance.VerifyBlobExists(tirilla);

            if (exists)
            {
                string encrypt = Cipher.EncryptString(tirilla, user.Id, true);
                return Json(encrypt, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult ObtenerSemanasCertificado()
        {
            IEnumerable<PeriodoReporte> periodo = _cadenaService.SolicitudEnvioReporteObtenerSemanas();

            return Json(periodo, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtenerMesesCertificado()
        {
            IEnumerable<PeriodoReporte> periodo = _cadenaService.SolicitudEnvioReporteObtenerMeses();

            return Json(periodo, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CrearReportes(string tipoReporte, int codCadena, int codCiclo)
        {
            try
            {
                SolicitudReporteResponse response = _puntoventaService.SolicitudEnvioReporte(new SolicitudReporte
                {
                    codUsuario = CrossController.Instance.GetUserInfoId(),
                    codCadena = codCadena,
                    codSolicitud = (byte)(TipoReporteEnum)Enum.Parse(typeof(TipoReporteEnum), tipoReporte),
                    codCiclo = codCiclo
                });

                if (response.HuboError)
                {
                    return Json(response.Mensaje, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json("Se ha generado el reporte. Será enviado a su correo electrónico registrado durante los próximos 60 minutos. Recuerde que la contraseña es su NIT sin dígito de verificación",
                        JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("Error durante la generación: " + ex, JsonRequestBehavior.AllowGet);
            }

        }
    }

}