namespace PlataformaVIA.Presentacion.Controllers
{
    using Core.Domain;
    using Core.Domain.Busqueda;
    using Core.Domain.PuntoDeVenta;
    using DataTables.Mvc;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using PlataformaVIA.Core.Domain.AdministradorDocumentos;
    using PlataformaVIA.Core.Domain.Parametro;
    using PlataformaVIA.Core.Domain.Reportes;
    using PlataformaVIA.Core.Domain.Seguridad;
    using PlataformaVIA.Presentacion.Filters;
    using PlataformaVIA.Presentacion.Models;
    using Presentacion.Helpers;
    using Presentacion.ViewModels.BusquedaAvanzada;
    using Services.Interfaces;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    [Authorize]
    [PasswordExpiredAuthorize]
    [NoCache]
    [SecurityLog]
    public class PuntoDeVentaController : Controller
    {
        private IPuntoVentaService _puntoventaService;
        private IAjusteService _ajusteService;
        private ICicloFacturacionService _ciclofacturacionService;
        private IParametroService _parametroService;
        private ICadenaService _cadenaService;

        #region Constructores
        public PuntoDeVentaController(IPuntoVentaService puntoventaService, ICicloFacturacionService ciclofacturacionService, IAjusteService ajusteService, IParametroService parametroService, ICadenaService cadenaService)
        {
            this._puntoventaService = puntoventaService;
            this._ciclofacturacionService = ciclofacturacionService;
            this._ajusteService = ajusteService;
            this._parametroService = parametroService;
            this._cadenaService = cadenaService;
        }
        #endregion

        #region "Punto de venta"

        public async Task<ActionResult> ConsultarPuntosdeVenta()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ConsultarPuntoDeVentaData([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, string tipoBusqueda = "")
        {
            try{

                var valorfiltro = requestModel.Search.Value.Trim();
                ResponseEO<PuntodeVenta> resultado = new ResponseEO<PuntodeVenta>
                {
                    IdUsuario = CrossController.Instance.GetUserInfoId(),
                    NumeroPagina = requestModel.Start / requestModel.Length,
                    TamanoPagina = requestModel.Length,
                    TextoBusqueda = valorfiltro,
                    FiltrosCriterio = new CriterioBusqueda { Filtro = tipoBusqueda }
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

                //query = query.OrderBy(orderByString ==
                //string.Empty ? "BarCode asc" : orderByString);

                #endregion Ordenado

                resultado = _puntoventaService.ConsultarPuntoDeVenta(resultado);

                var jsonResult = Json(new DataTablesResponse
                (requestModel.Draw, resultado.Entidades, resultado.TotalRegistros, resultado.TotalRegistros), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
                
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
                
        }

        #endregion


        [Authorize(Roles = "Punto de Venta")]
        public ActionResult Index()
        {

            try
            {
                ResponseIndividualEO<UsuarioPuntoDeVenta> usuario = new ResponseIndividualEO<UsuarioPuntoDeVenta>
                {
                    IdUsuario = CrossController.Instance.GetUserInfoId(),
                    IdBusqueda = 0
                };
                var usuarioPuntoDeVenta = _puntoventaService.ConsultarUsuarioPuntoDeVenta(usuario);

                if (usuarioPuntoDeVenta != null)
                    ViewBag.Id = usuarioPuntoDeVenta.Entidad.CODPUNTODEVENTA;

                return View();
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        [Authorize(Roles = "Punto de Venta")]
        public ActionResult ConsultarProductos()
        {
            try
            {
                ResponseIndividualEO<UsuarioPuntoDeVenta> usuario = new ResponseIndividualEO<UsuarioPuntoDeVenta>
                {
                    IdUsuario = CrossController.Instance.GetUserInfoId(),
                    IdBusqueda = 0
                };
                var usuarioPuntoDeVenta = _puntoventaService.ConsultarUsuarioPuntoDeVenta(usuario);

                if (usuarioPuntoDeVenta != null)
                    ViewBag.Id = usuarioPuntoDeVenta.Entidad.CODPUNTODEVENTA;

                return View();
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }


        // GET: PuntoVentas/Details/5
        public ActionResult Details(int id)
        {

            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            ////PuntodeVenta puntoVenta = db.PuntoVentas.Find(id);
            //var puntoVenta = _puntoventaService.GetDetallePuntoVenta(id.Value);
            //if (puntoVenta == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(puntoVenta);
            ViewBag.Id = id;
            return View();
        }

        public ActionResult MisTransacciones()
        {
            return View();
        }

        //[ChildActionOnly]
        //public ActionResult _DetallePuntoVenta(int id)
        //{
        //    ViewBag.MostrarNoficiaciones = false; //TODO EPINEDA 22/FEB/2018 Enviar por parámetro

        //    //if (id == null)
        //    //{
        //    //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    //}
        //    //PuntodeVenta puntoVenta = db.PuntoVentas.Find(id);
        //    var puntoVenta = _puntoventaService.GetDetallePuntoVenta(id);
        //    if (puntoVenta == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return PartialView(puntoVenta);

        //}

        //[ChildActionOnly]
        //public ActionResult _EstadoCuentaXLineaNegocio(int codpuntoventa)
        //{
        //    var estadocuentapuntoventa = _puntoventaService.GetEstadoCuentaLineaNegocio(codpuntoventa);
        //    if (estadocuentapuntoventa == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return PartialView(estadocuentapuntoventa);

        //}

        [ChildActionOnly]
        public ActionResult _EstadoCuentaXLineaNegocio(int codpuntoventa)
        {
            try
            {
                ResponseEO<EstadoCuentaXLineaDeNegocio> response = new ResponseEO<EstadoCuentaXLineaDeNegocio>
                {
                    IdUsuario = CrossController.Instance.GetUserInfoId()
                };

                response.FiltrosCriterio = new CriterioBusqueda { IdPadre = codpuntoventa };
                response = _puntoventaService.EstadosDeCuentaPorLineaDeNegocio(response);
                var estadocuentapuntoventa = response.Entidades;
                if (estadocuentapuntoventa == null)
                {
                    return HttpNotFound();
                }
                return PartialView(estadocuentapuntoventa);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }


        [ChildActionOnly]
        public ActionResult _PresupuestoXLineaNegocio(int codpuntoventa)
        {
            try
            {
                ResponseEO<PresupuestoXLineaDeNegocio> response = new ResponseEO<PresupuestoXLineaDeNegocio>();
                response.IdUsuario = CrossController.Instance.GetUserInfoId();
                response.FiltrosCriterio = new CriterioBusqueda { IdPadre = codpuntoventa };
                var resultado = _puntoventaService.PresupuestoPorLineaDeNegocio(response);

                var presupuesto = resultado.Entidades;
                if (presupuesto == null)
                {
                    return HttpNotFound();
                }
                return PartialView(presupuesto);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }


        public ActionResult Notificaciones()
        {
            try
            {
                ResponseEO<Notificaciones> response = new ResponseEO<Notificaciones>();

                response.IdUsuario = CrossController.Instance.GetUserInfoId();

                response = _puntoventaService.GetNotificaciones(response);
                if (response.Entidades == null)
                {
                    return HttpNotFound();
                }
                return PartialView("_Notificaciones", response.Entidades);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        [HttpPost]
        public ActionResult _DetalleProducto(int id, int esSubProducto)
        {
            try
            {
                ResponseIndividualEO<DetalleProducto> response = new ResponseIndividualEO<DetalleProducto>();
                ResponseIndividualEO<DetalleProducto> request = new ResponseIndividualEO<DetalleProducto>();
                request.IdBusqueda = id;
                request.IdUsuario = CrossController.Instance.GetUserInfoId();
                request.Entidad = new DetalleProducto { EsSubproducto = esSubProducto };
                response = _puntoventaService.ConsultarDetalleProducto(request);
                return PartialView(response.Entidad);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        #region Estado de Cuenta General

        public async Task<ActionResult> _ConsultarEstadoCuentaGeneral()
        {
            return PartialView();
        }

        [HttpGet]
        public ActionResult BusquedaAvanzadaEstadoCuentaGeneral()
        {
            var busquedaavanzadaVM = new BusquedaAvanzadaCicloFacturacionViewModel
            {
                CicloFacturacionList = new SelectList(_ciclofacturacionService.GetCicloFacturacion(false),
                                                                        "Id",
                                                                        "RangoFechaFacturacion")
            };
            return View("_BusquedaAvanzadaEstadoCuentaGeneral", busquedaavanzadaVM);
        }

        [HttpPost]
        public ActionResult GetFiltroAvanzadoEstadoCuentaGeneral([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, BusquedaAvanzadaCicloFacturacionViewModel searchViewModel)
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
                ResponseEO<EstadoDeCuentaGeneral> response = new ResponseEO<EstadoDeCuentaGeneral>();
                var criterioBusqueda = new CriterioBusquedaCicloFacturacion { IdPadre = searchViewModel.CodPuntoVenta, Filtro = valorfiltro, Paginacion = new ParametroPaginacion { NumeroPagina = requestModel.Start / requestModel.Length, TamanoPagina = requestModel.Length } };
                response.FiltrosCicloFacturacion = criterioBusqueda;
                response.IdUsuario = CrossController.Instance.GetUserInfoId();
                response = _puntoventaService.GetEstadoCuentaGeneral(response);

                return Json(new DataTablesResponse
                (requestModel.Draw, response.Entidades, response.FiltrosCicloFacturacion.Paginacion.TotalRegistros, response.FiltrosCicloFacturacion.Paginacion.TotalRegistros),
                            JsonRequestBehavior.DenyGet);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        public ActionResult _ResumenPagoTotalFecha(int id, bool esPagoMinimo)
        {
            try
            {
                ResponseEO<ResumenPago> response = new ResponseEO<ResumenPago>();
                response.IdUsuario = CrossController.Instance.GetUserInfoId();
                response.FiltrosCriterio = new CriterioBusqueda { IdPadre = id };
                response = _puntoventaService.GetResumenPagoTotal(response, esPagoMinimo);
                return PartialView(response.Entidades);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        //public ActionResult _ResumenCuadreSemanal(int id, bool esPagoMinimo)
        //{
        //    ResponseEO<ResumenCuadreSemanal> response = new ResponseEO<ResumenCuadreSemanal>();
        //    response.IdUsuario = CrossController.Instance.GetUserInfoId();
        //    response.FiltrosCriterio = new CriterioBusqueda { IdPadre = id };
        //    response = _puntoventaService.GetResumenCuadreSemanal(response);
        //    return PartialView(response.Entidades);
        //}

        public ActionResult _ResumenCuadreSemanal(int id, bool esPagoMinimo)
        {
            try
            {
                ResumenCuadreSemanal response = new ResumenCuadreSemanal()
                {
                    CodUsuario = CrossController.Instance.GetUserInfoId(),
                    Id = id,
                    EsPagoMinimo = esPagoMinimo ? 1 : 0
                };

                return PartialView(_puntoventaService.GetResumenCuadreSemanal(response));
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        public ActionResult _ResumenVentasAjustesFacturacion(int id, bool esPagoMinimo)
        {
            try
            {
                ResponseEO<ResumenCuadreSemanal> response = new ResponseEO<ResumenCuadreSemanal>();
                response.IdUsuario = CrossController.Instance.GetUserInfoId();
                response.FiltrosCriterio = new CriterioBusqueda { IdPadre = id };
                response = _puntoventaService.GetResumenVentasAjusteFacturacion(response, esPagoMinimo);
                return PartialView(response.Entidades);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        public ActionResult _ResumenPagosRetirosFacturacion(int id, bool esPagoMinimo)
        {
            try
            {
                ResponseEO<ResumenPagosRetirosSemanaActual> response = new ResponseEO<ResumenPagosRetirosSemanaActual>();
                response.IdUsuario = CrossController.Instance.GetUserInfoId();
                response.FiltrosCriterio = new CriterioBusqueda { IdPadre = id };
                response = _puntoventaService.GetResumenPagosRetiros(response, esPagoMinimo);
                return PartialView(response.Entidades);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        public ActionResult _ResumenAjustesCartera(int id, bool esPagoMinimo)
        {
            try
            {
                ResponseEO<ResumenAjustesCartera> response = new ResponseEO<ResumenAjustesCartera>();
                response.IdUsuario = CrossController.Instance.GetUserInfoId();
                response.FiltrosCriterio = new CriterioBusqueda { IdPadre = id };
                response = _puntoventaService.GetResumenAjustesCartera(response, esPagoMinimo);
                return PartialView(response.Entidades);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        public ActionResult _DetallePagoTotalYMinimoFecha(int id, bool esPagoMinimo)
        {
            ViewBag.Id = id;
            ViewBag.EsPagoMinimo = esPagoMinimo;
            return PartialView();
        }

        public ActionResult _PuntoVentaInfoGeneral(PuntodeVenta puntodeVenta)
        {
            return PartialView(puntodeVenta);
        }

        #endregion

        #region Tirilla Cuadre  Semanal
        public async Task<ActionResult> _ConsultarTirillaCuadreSemanal()
        {
            return PartialView();
        }

        [HttpGet]
        public ActionResult BusquedaAvanzadaTirillaCuadreSemanal()
        {
            try
            {
                var busquedaavanzadaVM = new BusquedaAvanzadaCicloFacturacionViewModel
                {
                    CicloFacturacionList = new SelectList(_ciclofacturacionService.GetCicloFacturacion(false),
                                                                            "Id",
                                                                            "RangoFechaFacturacion")
                };
                return View("_BusquedaAvanzadaTirillaCuadreSemanal", busquedaavanzadaVM);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        [HttpPost]
        public ActionResult GetFiltroAvanzadoTirillaCuadreSemanal([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, BusquedaAvanzadaCicloFacturacionViewModel searchViewModel)
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

                #endregion Ordenado

                var criterioBusqueda = new CriterioBusquedaCicloFacturacion { CodUsuario = CrossController.Instance.GetUserInfoId(), IdPadre = searchViewModel.CodPuntoVenta, Filtro = valorfiltro, Paginacion = new ParametroPaginacion { NumeroPagina = requestModel.Start / requestModel.Length, TamanoPagina = requestModel.Length } };

                var lstcadenas = _puntoventaService.GetTirillaCuadreSemanal(criterioBusqueda);

                var jsonResult = Json(new DataTablesResponse
                (requestModel.Draw, lstcadenas, criterioBusqueda.Paginacion.TotalRegistros, criterioBusqueda.Paginacion.TotalRegistros), JsonRequestBehavior.DenyGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        #endregion

        #region Mis Ventas
        public async Task<ActionResult> _ConsultarVentas(PuntodeVenta puntodeVenta)
        {
            return PartialView(puntodeVenta);
        }

        public async Task<ActionResult> _ConsultarProductos(PuntodeVenta puntodeVenta)
        {
            return PartialView(puntodeVenta);
        }

        public async Task<ActionResult> _ConsultarTransacciones()
        {
            return PartialView();
        }

        public async Task<ActionResult> _ConsultarPagos(PuntodeVenta puntodeVenta)
        {
            return PartialView(puntodeVenta);
        }

        [HttpGet]
        public ActionResult BusquedaAvanzadaMisVentas()
        {
            var busquedaavanzadaVM = new BusquedaAvanzadaRangoFechasViewModel { FechaInicio = DateTime.Now.StartOfWeek(DayOfWeek.Sunday) };
            busquedaavanzadaVM.FechaFin = busquedaavanzadaVM.FechaInicio.AddDays(6);

            return View("_BusquedaAvanzadaMisVentas", busquedaavanzadaVM);
        }

        [HttpGet]
        public ActionResult BusquedaAvanzadaPorFecha()
        {
            var busquedaavanzadaVM = new BusquedaAvanzadaRangoFechasViewModel();

            return View("_BusquedaAvanzadaPorFecha", busquedaavanzadaVM);
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

        public ActionResult _DetallePago(int id)
        {
            ViewBag.IdDetallePago = id;
            return PartialView();
        }

        public ActionResult _DetalleAjuste(int id)
        {
            ViewBag.Id = id;
            return PartialView();
        }

        [HttpPost]
        public ActionResult GetFiltroAvanzadoMisVentas([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, BusquedaAvanzadaRangoFechasViewModel searchViewModel)
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

            //Verificar si es la primera carga => Setear fechas de esta semana 
            if (searchViewModel.FechaInicio == DateTime.MinValue && searchViewModel.FechaFin == DateTime.MinValue)
            {
                searchViewModel.FechaInicio = DateTime.Now.StartOfWeek(DayOfWeek.Sunday);
                searchViewModel.FechaFin = searchViewModel.FechaInicio.AddDays(6);
            }

            var criterioBusqueda = new CriterioBusquedaFechas { IdPadre = searchViewModel.CodProducto, FechaInicio = searchViewModel.FechaInicio, FechaFin = searchViewModel.FechaFin, Filtro = valorfiltro, Paginacion = new ParametroPaginacion { NumeroPagina = requestModel.Start, TamanoPagina = requestModel.Length } };
            var response = new ResponseEO<Ventas> { FiltrosFechas = criterioBusqueda, NumeroPagina = requestModel.Start / requestModel.Length, TamanoPagina = requestModel.Length };
            response.IdUsuario = CrossController.Instance.GetUserInfoId();
            response = _puntoventaService.GetMisVentas(response);
            var lstcadenas = response.Entidades;

            return Json(new DataTablesResponse
            (requestModel.Draw, lstcadenas, response.TotalRegistros, response.TotalRegistros),
                        JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult GetFiltroAvanzadoMisProductos([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, BusquedaAvanzadaCicloFacturacionViewModel busqueda)
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


            var criterioBusqueda = new CriterioBusquedaFechas { IdPadre = busqueda.CodPuntoVenta, Filtro = valorfiltro, Paginacion = new ParametroPaginacion { NumeroPagina = requestModel.Start / requestModel.Length, TamanoPagina = requestModel.Length } };
            var response = new ResponseEO<Producto> { IdUsuario = CrossController.Instance.GetUserInfoId(), FiltrosFechas = criterioBusqueda, NumeroPagina = requestModel.Start / requestModel.Length, TamanoPagina = requestModel.Length };
            response = _puntoventaService.GetMisProductos(response);
            var lstcadenas = response.Entidades;

            return Json(new DataTablesResponse
            (requestModel.Draw, lstcadenas, response.TotalRegistros, response.TotalRegistros),
                        JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult GetFiltroAvanzadoMisTransacciones([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, BusquedaAvanzadaRangoFechasViewModel searchViewModel)
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

            //Verificar si es la primera carga => Setear fechas de esta semana 
            if (searchViewModel.FechaInicio == DateTime.MinValue && searchViewModel.FechaFin == DateTime.MinValue)
            {
                searchViewModel.FechaInicio = DateTime.Now.StartOfWeek(DayOfWeek.Sunday);
                searchViewModel.FechaFin = searchViewModel.FechaInicio.AddDays(6);
            }

            var criterioBusqueda = new CriterioBusquedaFechas { IdPadre = searchViewModel.CodProducto, FechaInicio = searchViewModel.FechaInicio, FechaFin = searchViewModel.FechaFin, Filtro = valorfiltro, Paginacion = new ParametroPaginacion { NumeroPagina = requestModel.Start / requestModel.Length, TamanoPagina = requestModel.Length } };
            var response = new ResponseEO<Transaccion> { FiltrosFechas = criterioBusqueda, NumeroPagina = requestModel.Start / requestModel.Length, TamanoPagina = requestModel.Length };
            response.IdUsuario = CrossController.Instance.GetUserInfoId();
            response = _puntoventaService.GetMisTransacciones(response);
            var lstcadenas = response.Entidades;

            return Json(new DataTablesResponse
            (requestModel.Draw, lstcadenas, response.TotalRegistros, response.TotalRegistros),
                        JsonRequestBehavior.DenyGet);
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
            response = _puntoventaService.GetMisPagos(response);
            var lstcadenas = response.Entidades;

            var jsonResult = Json(new DataTablesResponse
            (requestModel.Draw, lstcadenas, response.TotalRegistros, response.TotalRegistros), JsonRequestBehavior.DenyGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult GetFiltroAvanzadoDetallePago([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, BusquedaAvanzadaCicloFacturacionViewModel searchViewModel)
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
            var criterio = new CriterioBusqueda { IdPadre = searchViewModel.CodPuntoVenta };
            var response = new ResponseEO<DistribucionDetallePago> { NumeroPagina = requestModel.Start / requestModel.Length, TamanoPagina = requestModel.Length, FiltrosCriterio = criterio };
            response = _puntoventaService.GetDetallePago(response);
            var lstcadenas = response.Entidades;

            if (lstcadenas.First() != null)
            {
                lstcadenas.First().ValorOtros = String.Format("{0:C}", response.DetallesPago.ValorOtros);
                lstcadenas.First().ValorSinIdentificar = String.Format("{0:C}", response.DetallesPago.ValorSinIDentificar);
            }

            var jsonResult = Json(new DataTablesResponse
            (requestModel.Draw, lstcadenas, response.TotalRegistros, response.TotalRegistros), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult GetFiltroAvanzadoDetalleAjuste([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, BusquedaAvanzadaCicloFacturacionViewModel searchViewModel)
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
            var criterio = new CriterioBusqueda { IdPadre = searchViewModel.CodPuntoVenta };
            var response = new ResponseEO<DistribucionDetalleAjuste> { NumeroPagina = requestModel.Start / requestModel.Length, TamanoPagina = requestModel.Length, FiltrosCriterio = criterio };
            response = _puntoventaService.GetDetalleAjuste(response);
            var lstcadenas = response.Entidades;

            if (lstcadenas.First() != null)
            {
                lstcadenas.First().FechaHora = response.DetallesAjuste.FechaHora;
                lstcadenas.First().Valor = String.Format("{0:C}", response.DetallesAjuste.Valor);
                lstcadenas.First().Categoria = response.DetallesAjuste.Categoria;
                lstcadenas.First().Descripcion = response.DetallesAjuste.Descripcion;
            }

            var jsonResult = Json(new DataTablesResponse
            (requestModel.Draw, lstcadenas, response.TotalRegistros, response.TotalRegistros), JsonRequestBehavior.AllowGet);
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
            response = _puntoventaService.GetMisAjustes(response);
            var lstcadenas = response.Entidades;

            return Json(new DataTablesResponse
            (requestModel.Draw, lstcadenas, response.TotalRegistros, response.TotalRegistros),
                        JsonRequestBehavior.DenyGet);
        }
        #endregion

        public ActionResult GetPartialView(string idlink, int idpuntoventa)
        {
            try
            {
                ViewBag.MostrarNoficiaciones = false;
                switch (idlink)
                {
                    case "Inicio":
                        ResponseIndividualEO<PuntodeVenta> puntoDeVenta = new ResponseIndividualEO<PuntodeVenta>();
                        puntoDeVenta.IdUsuario = CrossController.Instance.GetUserInfoId();
                        puntoDeVenta.IdBusqueda = idpuntoventa;
                        var puntoVenta = _puntoventaService.ConsultarInformacionPuntoDeVenta(puntoDeVenta);
                        if (puntoVenta == null)
                        {
                            return HttpNotFound();
                        }
                        return PartialView("_DetallePuntoVenta", puntoVenta.Entidad);
                    case "Encabezado":
                        ResponseIndividualEO<PuntodeVenta> pdv = new ResponseIndividualEO<PuntodeVenta>();
                        pdv.IdUsuario = CrossController.Instance.GetUserInfoId();
                        pdv.IdBusqueda = idpuntoventa;
                        var puntodeVenta = _puntoventaService.ConsultarInformacionPuntoDeVenta(pdv);
                        if (puntodeVenta == null)
                        {
                            return HttpNotFound();
                        }
                        return Json(puntodeVenta.Entidad, JsonRequestBehavior.AllowGet);
                    case "EstadoCuenta":
                        return PartialView("_ConsultarEstadoCuentaGeneral", new PuntodeVenta { Id = idpuntoventa });
                    case "TirillaCuadreSemanal":
                        return PartialView("_ConsultarTirillaCuadreSemanal", new PuntodeVenta { Id = idpuntoventa });
                    case "Ventas":
                        return PartialView("_ConsultarVentas", new PuntodeVenta { Id = idpuntoventa });
                    case "Productos":
                        return PartialView("_ConsultarProductos", new PuntodeVenta { Id = idpuntoventa });

                    case "Transacciones":
                        return PartialView("_ConsultarTransacciones", new PuntodeVenta { Id = idpuntoventa });
                    case "Pagos":
                        return PartialView("_ConsultarPagos", new PuntodeVenta { Id = idpuntoventa });
                    case "Ajustes":
                        return PartialView("_ConsultarAjustes", new PuntodeVenta { Id = idpuntoventa });
                    case "Reportes":                        
                        return PartialView("_ConsultarReportes", new BusquedaAvanzadaCicloFacturacionViewModel { CodPuntoVenta = idpuntoventa});
                    case "Politica":
                        
                        ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

                        ImagenesInstructivo politica = _puntoventaService.ConsultarPoliticaPago(CrossController.Instance.GetUserInfoId(), idpuntoventa);

                        politica.Ubicacion = Cipher.EncryptString(politica.Ubicacion, user.Id, true);

                        return PartialView("_PoliticaPago", politica);
                }

                return RedirectToAction("YourHomePage");
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        [HttpPost]
        public JsonResult ObtenerCicloSemanalEstadodeCuenta()
        {
            var cicloList = new SelectList(_ciclofacturacionService.GetCicloFacturacion(false),"Id","RangoFechaFacturacion");

            return Json(cicloList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtenerCicloDiarioEstadodeCuenta()
        {
            var cicloList = new SelectList(_ciclofacturacionService.GetCicloFacturacion(true), "Id", "RangoParaReportesDiarios");

            return Json(cicloList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtenerCicloSemanalPrefacturacion()
        {
            var cicloList = new SelectList(_ciclofacturacionService.GetCicloFacturacion(true), "Id", "RangoFechaFacturacion");

            return Json(cicloList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtenerMesesPrefacturacion()
        {
            var mesesList = new SelectList(_cadenaService.SolicitudEnvioReporteObtenerMeses(), "Id", "Periodo");
            
            return Json(mesesList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> GenerarTirillaPDV(string ubicacion)
        {
            ubicacion = HttpUtility.UrlDecode(ubicacion);

            Parametro parametro = _parametroService.BuscarParametro(CrossController.Instance.GetUserInfoId(), "RutaTirillasPDV");

            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            string[] rutaCompleta = ubicacion.Trim().Split('_');

            
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
        public JsonResult CrearReportes(string tipoReporte, int codPDV, int? codCiclo, DateTime? fechaReferencia)
        {
            try
            {
                SolicitudReporteResponse response = _puntoventaService.SolicitudEnvioReporte(new SolicitudReporte
                {
                    codUsuario = CrossController.Instance.GetUserInfoId(),
                    codPDV = codPDV,
                    codSolicitud = (byte)(TipoReporteEnum)Enum.Parse(typeof(TipoReporteEnum), tipoReporte),
                    codCiclo = codCiclo,
                    fechaReferencia = fechaReferencia
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

        public ActionResult FiltroBusqueda()
        {

            var busquedaavanzadaVM = new BusquedaAvanzadaFiltrosViewModel
            {
                OpcionesBusquedaList = new SelectList(new[]
                    {
                        new { ID="puntoVenta", Name="Punto de Venta" },
                        new { ID="cadena", Name="Cadena" },
                    }, "ID", "Name", 0)
            };


            return PartialView(busquedaavanzadaVM);
        }
    }
}
