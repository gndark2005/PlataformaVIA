namespace PlataformaVIA.Presentacion.Controllers
{
    using Core.Domain;
    using Core.Domain.Busqueda;
    using Core.Domain.Colocador;
    using Core.Domain.General;
    using Core.Domain.PuntoDeVenta;
    using Core.Domain.RepresentanteLegal;
    using DataTables.Mvc;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using PlataformaVIA.Core.Domain.Seguridad;
    using PlataformaVIA.Presentacion.Filters;
    using Presentacion.Helpers;
    using Services.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    [Authorize]
    [PasswordExpiredAuthorize]
    [NoCache]
    public class ColocadorController : Controller
    {
        private IColocadorService _colocadorService;
        private ISeguridadService _seguridadService;

        #region Constructores
        public ColocadorController(IColocadorService colocadorService,  ISeguridadService seguridadService)
        {
            this._colocadorService = colocadorService;
            this._seguridadService = seguridadService;
        }
        #endregion

        public async Task<ActionResult> ConsultarColocadores()
        {
            return View();
        }

        // GET: Colocador
        [SecurityLog]
        public ActionResult _DetalleColocador(int id, int sincronizar)
        {
            try
            {
                ResponseIndividualEO<Colocador> response = new ResponseIndividualEO<Colocador>();
                CriterioBusqueda Request = new CriterioBusqueda();
                Request.IdPadre = id;
                Request.Filtro = sincronizar.ToString();
                Request.IdUsuario = CrossController.Instance.GetUserInfoId();
                response = _colocadorService.GetDetalleColocador(Request);
                var listaOpciones = new List<SelectListItem>();
                listaOpciones.Add(new SelectListItem { Text = "Si", Value = true.ToString() });
                listaOpciones.Add(new SelectListItem { Text = "No", Value = false.ToString() });
                ViewBag.OtorgarAcceso = listaOpciones;
                //ViewBag.CodPago = idcolocador;
                return PartialView(response.Entidad);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        [SecurityLog]
        public ActionResult _CrearColocador()
        {
            try
            {
                IEnumerable<TipoIdentificacion> tiposIdentificacion = _colocadorService.GetTiposIdentificacion();
                ViewBag.TiposIdentificacion = tiposIdentificacion.Select(a => new SelectListItem { Text = a.Nombre, Value = a.Id.ToString() });

                IEnumerable<Genero> generos = _colocadorService.GetGeneros();
                ViewBag.Generos = generos.Select(a => new SelectListItem { Text = a.Nombre, Value = a.Id.ToString() });

                IEnumerable<TipoSangre> tiposSangre = _colocadorService.GetTiposSangre();
                ViewBag.TiposSangre = tiposSangre.Select(a => new SelectListItem { Text = a.Nombre, Value = a.Id.ToString() });

                IEnumerable<TipoVendedor> tiposVendedor = _colocadorService.GetTiposVendedor();
                ViewBag.TiposVendedor = tiposVendedor.Select(a => new SelectListItem { Text = a.Nombre, Value = a.Id.ToString() });

                Colocador colocador = new Colocador();
                return PartialView(colocador);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _CrearColocador(Colocador form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    object resultado = null;
                    try
                    {
                        int codUsuario = CrossController.Instance.GetUserInfoId();

                        var User = _seguridadService.GetUsuarioInfo(form.CorreoElectronico);

                        if (User.ID_USUARIOINFO == 0)
                        {
                            Colocador obj = new Colocador() { NumeroIdentificacion = form.NumeroIdentificacion };
                            var Validate = _colocadorService.Colocadores_GetDetalleColocadorXNumeroIdentificacion(obj);

                            //if (Validate.Entidades.Count() == 0)
                            //{
                                int idColocador = _colocadorService.CreateColocador(form, codUsuario);

                                if (idColocador > 0)
                                {
                                    resultado = new { Error = false, Mensaje = "Exitoso", idColocador = idColocador };
                                }
                            //}
                            //else
                            //{
                            //    resultado = new { Error = true, Mensaje = "El Numero de Identificacion ingresado ya se encuentra registrado en el sistema.", idColocador = -1 };
                            //    return Json(resultado, JsonRequestBehavior.DenyGet);
                            //}
                        }
                        else
                        {
                            resultado = new { Error = true, Mensaje = "El Correo ingresado ya se encuentra registrado en el sistema.", idColocador = -1 };
                            return Json(resultado, JsonRequestBehavior.DenyGet);
                        }

                    }
                    catch (Exception ex)
                    {
                        resultado = new { Error = true, Mensaje = "Error: " + ex.Message, idColocador = -1 };
                    }

                    return Json(resultado, JsonRequestBehavior.DenyGet);
                }
                else
                {
                    var resultado = new { Error = true, Mensaje = "Error de validación de los datos, por favor diligencie los campos requeridos", idColocador = -1 };
                    return Json(resultado, JsonRequestBehavior.DenyGet);
                }
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        [SecurityLog]
        public ActionResult _EditarColocador(int id, int sincronizar)
        {
            try
            {
                IEnumerable<TipoIdentificacion> tiposIdentificacion = _colocadorService.GetTiposIdentificacion();
                ViewBag.TiposIdentificacion = tiposIdentificacion.Select(a => new SelectListItem { Text = a.Nombre, Value = a.Id.ToString() });

                IEnumerable<Genero> generos = _colocadorService.GetGeneros();
                ViewBag.Generos = generos.Select(a => new SelectListItem { Text = a.Nombre, Value = a.Id.ToString() });

                IEnumerable<TipoSangre> tiposSangre = _colocadorService.GetTiposSangre();
                ViewBag.TiposSangre = tiposSangre.Select(a => new SelectListItem { Text = a.Nombre, Value = a.Id.ToString() });

                IEnumerable<TipoVendedor> tiposVendedor = _colocadorService.GetTiposVendedor();
                ViewBag.TiposVendedor = tiposVendedor.Select(a => new SelectListItem { Text = a.Nombre, Value = a.Id.ToString() });

                ResponseIndividualEO<Colocador> response = new ResponseIndividualEO<Colocador>();
                CriterioBusqueda request = new CriterioBusqueda();
                request.IdPadre = id;
                request.IdUsuario = CrossController.Instance.GetUserInfoId();
                request.Filtro = sincronizar.ToString();
                
                response = _colocadorService.GetDetalleColocador(request);
                
                return PartialView(response.Entidad);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _EditarColocador(Colocador form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    object resultado = null;
                    try
                    {
                        int codUsuario = CrossController.Instance.GetUserInfoId();
                        bool guardadoExitoso = _colocadorService.EditColocador(form, codUsuario);
                        if (guardadoExitoso == true)
                        {
                            resultado = new { Error = false, Mensaje = "Exitoso" };
                        }
                    }
                    catch (Exception ex)
                    {
                        var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                    }

                    return Json(resultado, JsonRequestBehavior.DenyGet);
                }
                else
                {
                    var resultado = new { Error = true, Mensaje = "Error de validación de los datos, por favor diligencie los campos requeridos." };
                    return Json(resultado, JsonRequestBehavior.DenyGet);
                }
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [SecurityLog]
        public ActionResult _EliminarColocador(CriterioBusqueda form)
        {
            object resultado = null;
            try
            {
                form.IdUsuario = CrossController.Instance.GetUserInfoId();
                bool guardadoExitoso = _colocadorService.DeleteColocador(form);
                if (guardadoExitoso == true)
                {
                    resultado = new { Error = false, Mensaje = "Exitoso" };
                }
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }

            return Json(resultado, JsonRequestBehavior.DenyGet);
        }

        [SecurityLog]
        public ActionResult _AccesoColocador(int id, int sincronizar)
        {
            try
            {              
                ResponseIndividualEO<Colocador> response = new ResponseIndividualEO<Colocador>();
                CriterioBusqueda Request = new CriterioBusqueda();
                Request.IdPadre = id;
                Request.Filtro = sincronizar.ToString();
                Request.IdUsuario = CrossController.Instance.GetUserInfoId();
                response = _colocadorService.GetDetalleColocador(Request);

                if (response.Entidad.NombreCompleto.Trim().Length > 0)
                {
                    var listaOpciones = new List<SelectListItem>();
                    listaOpciones.Add(new SelectListItem { Text = "Si", Value = true.ToString() });
                    listaOpciones.Add(new SelectListItem { Text = "No", Value = false.ToString() });
                    ViewBag.OtorgarAcceso = listaOpciones;
                    return PartialView(response.Entidad);
                }
                else
                {
                    object param = null;
                    param = new { id = id, sincronizar = sincronizar  };
                    return RedirectToAction("_EditarColocador", param);
                }
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SecurityLog]
        public ActionResult _OtorgarAccesoIndividual(List<AsignacionPuntoDeVenta> asignaciones, bool accesoGlobal, int codColocador)
        {
            object resultado = null;
            try
            {
                int codUsuario = CrossController.Instance.GetUserInfoId();

                if (accesoGlobal == true)
                {
                    bool guardadoExitosoGlobal = _colocadorService.OtorgarAccesoGlobalAColocador(codColocador, codUsuario);
                    if (guardadoExitosoGlobal == true)
                    {
                        resultado = new { Error = false, Mensaje = "Exitoso" };
                    }
                    // Guardamos el estado en colocador y si es el caso limpiamos las asignaciones
                }
                else if (accesoGlobal == false && asignaciones != null && asignaciones.Count > 0)
                {
                    // Guardamos el estado en el colocador y asignamos cada uno de los puntos de venta a los que tienen permisos
                    bool guardadoExitosoPorPDV = _colocadorService.OtorgarAccesoPorPuntoDeVenta(asignaciones, codUsuario);
                    if (guardadoExitosoPorPDV == true)
                    {
                        resultado = new { Error = false, Mensaje = "Exitoso" };
                    }
                }
                else
                {
                    resultado = new { Error = true, Mensaje = "Ha ocurrido un error en la validacion de los datos" };
                }
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }

            return Json(resultado, JsonRequestBehavior.DenyGet);
        }

        // GET: Puntos de venta Colocador
        [SecurityLog]
        public async Task<ActionResult> _PuntosVentaAccesoColocador()
        {
            return PartialView();
        }

        public async Task<ActionResult> _EditarPuntosVentaAccesoColocador()
        {
            return PartialView();
        }

        [SecurityLog]
        public ActionResult GetFiltroBasicoPuntosAcceso([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, int idcolocador)
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
                ResponseEO<PuntodeVenta> response = new ResponseEO<PuntodeVenta>();
                response.IdUsuario = CrossController.Instance.GetUserInfoId();
                CriterioBusqueda criterioBusqueda = new CriterioBusqueda { IdPadre = idcolocador, Filtro = valorfiltro, Paginacion = new ParametroPaginacion { NumeroPagina = requestModel.Start, TamanoPagina = requestModel.Length } };
                response.FiltrosCriterio = criterioBusqueda;

                var lstcolocadores = _colocadorService.GetPuntosVentaAccesoColocador(response);
                //var lstcolocadores = _cadenaService.GetCadenaXRazonSocial(criterioBusqueda);


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
        //public ActionResult _PuntosVentaAccesoColocador()
        //{
        //    var detallecolocador = _colocadorService.GetPuntosVentaAccesoColocador(id);
        //    //ViewBag.CodPago = idcolocador;
        //    return PartialView(detallecolocador);
        //}

        [SecurityLog]
        public ActionResult GetFiltroBasicoColocadores([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
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
                ResponseEO<Colocador> response = new ResponseEO<Colocador>();
                response.IdUsuario = CrossController.Instance.GetUserInfoId();
                response.FiltrosCriterio = new CriterioBusqueda { Filtro = valorfiltro, Paginacion = new ParametroPaginacion { NumeroPagina = requestModel.Start /requestModel.Length, TamanoPagina = requestModel.Length } };


                response = _colocadorService.GetColocadoresXRazonSocial(response);
                //var lstcolocadores = _cadenaService.GetCadenaXRazonSocial(criterioBusqueda);


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

        public ActionResult GetCiudadesByFilter(string filtro)
        {
            try
            {
                IEnumerable<Ciudad> ciudades = new List<Ciudad>();
                if (filtro.Length >= 3)
                {
                    ciudades = _colocadorService.GetCiudadesByFilter(filtro);
                }

                return Json(ciudades, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }
    }
}