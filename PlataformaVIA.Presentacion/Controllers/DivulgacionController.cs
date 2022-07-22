using DataTables.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PlataformaVIA.Core.Domain;
using PlataformaVIA.Core.Domain.Busqueda;
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
    public class DivulgacionController : Controller
    {
        private IDivulgacionService _divulgacionService;

        #region Constructores
        public DivulgacionController(IDivulgacionService divulgacionService)
        {
            this._divulgacionService = divulgacionService;
        }
        #endregion

        // GET: Divulgacion
        public ActionResult Index()
        {
            return View();
        }

        //[Authorize(Roles = "Admin")]        
        public ActionResult GetFiltroBasicoDivulgaciones([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
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
                ResponseEO<Divulgacion> response = new ResponseEO<Divulgacion>();
                response.IdUsuario = CrossController.Instance.GetUserInfoId();
                response.FiltrosCriterio = new CriterioBusqueda { Filtro = valorfiltro, Paginacion = new ParametroPaginacion { NumeroPagina = requestModel.Start / requestModel.Length, TamanoPagina = requestModel.Length } };

                response = _divulgacionService.GetDivulgaciones(response);


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

        public ActionResult DetalleDivulgacion(int id)
        {
            try
            {
                ResponseIndividualEO<Divulgacion> response = new ResponseIndividualEO<Divulgacion>();
                CriterioBusqueda Request = new CriterioBusqueda();
                Request.IdPadre = id;
                Request.IdUsuario = CrossController.Instance.GetUserInfoId();
                response = _divulgacionService.GetDetallesDivulgacion(Request);

                List<Rol> roles = _divulgacionService.GetRoles().ToList();
                ViewBag.Roles = roles.Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() });


                string[] listroles;
                string[] lisexcep;

                var getroles = _divulgacionService.ObtenerRolesxDivulgacion(id);
                var getexcep = _divulgacionService.ObtenerExcepcionesXDivulgacion(id);

                if (getroles != null && getroles.Count() > 0)
                {
                    listroles = getroles.Select(s => s.CodDRol).ToList<string>().ToArray();
                    response.Entidad.ROLES = listroles;
                }

                if (getexcep != null && getexcep.Count() > 0)
                {
                    lisexcep = getexcep.Select(s => s.NIT).ToList<string>().ToArray();
                    response.Entidad.EXCEPCIONESNIT = lisexcep;

                    ViewBag.Exceptions = getexcep.Select(a => new SelectListItem { Text = a.NIT, Value = a.NIT });
                }
                else
                {


                    ViewBag.Exceptions = new List<SelectListItem>();
                }


                return PartialView(response.Entidad);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        public ActionResult CrearDivulgacion()
        {
            try
            {
                Divulgacion model = new Divulgacion();

                List<Rol> roles = _divulgacionService.GetRoles().ToList();               
                ViewBag.Roles = roles.Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() });

                model.FECHAINICIO = DateTime.Now;
                model.FECHAFIN = DateTime.Now;
                return View(model);
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }
        }

        [HttpPost]       
        [Authorize(Roles = "Admin")]
        public ActionResult ObtenerNITS(string search)
        {
            try
            {
                List<NitDivulgacion> result = new List<NitDivulgacion>();

                 result =  _divulgacionService.ObtenerNITS(search).ToList();

                return Json(result, JsonRequestBehavior.AllowGet);
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
        public ActionResult CrearDivulgacion(Divulgacion model)
        {
            if (ModelState.IsValid)
            {
               
                try
                {
                    int idUser = CrossController.Instance.GetUserInfoId();
                    model.CODUSUARIOINFOMODIFICACION = idUser;
                    model.HABILITADA = true;

                    int idDiv = _divulgacionService.AgregarDivulgacion(model);

                    if(idDiv != 0)
                    {
                        List<string> roles = model.ROLES.ToList<string>();

                        foreach(var rol in roles)
                        {
                            int DivxRol = _divulgacionService.AgregarDivulgacionxRol(new DivulgacionxRol { CodDivulgacion = idDiv, CodDRol = rol});
                        }


                        if(model.EXCEPCIONESNIT != null)
                        {
                            List<string> excepciones = model.EXCEPCIONESNIT.ToList<string>();

                            foreach (var exp in excepciones)
                            {
                                int DivxEx = _divulgacionService.AgregarExcepcionxNIT(new ExcepcionxNIT { CodDivulgacion = idDiv, NIT = exp });
                            }
                        }

                        
                    }
                    else
                    {
                        List<Rol> roles = _divulgacionService.GetRoles().ToList();
                        ViewBag.Roles = roles.Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() });

                        ModelState.AddModelError("", "Se produjo un error, intente nuevamente.");
                    }

                }
                catch (Exception ex)
                {
                    var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                    return RedirectToAction("ErrorPage", "Account", exception);
                }
                //ViewBag.InstructivoMessage = "Creado";
                return RedirectToAction("Index");
            }
            else
            {
                List<Rol> roles = _divulgacionService.GetRoles().ToList();
                ViewBag.Roles = roles.Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() });

                ModelState.AddModelError("", "Por favor verifique los campos");
            }
            return View(model);
        }

        public ActionResult ModificarDivulgacion(int id)
        {
            try
            {
                            
                ResponseIndividualEO<Divulgacion> response = new ResponseIndividualEO<Divulgacion>();
                CriterioBusqueda Request = new CriterioBusqueda();
                Request.IdPadre = id;
                Request.IdUsuario = CrossController.Instance.GetUserInfoId();
                response = _divulgacionService.GetDetallesDivulgacion(Request);

                List<Rol> roles = _divulgacionService.GetRoles().ToList();
                ViewBag.Roles = roles.Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() });

              
                string[] listroles;
                string[] lisexcep;

                var getroles = _divulgacionService.ObtenerRolesxDivulgacion(id);
                var getexcep = _divulgacionService.ObtenerExcepcionesXDivulgacion(id);

                if (getroles != null && getroles.Count() > 0)
                {
                    listroles = getroles.Select(s => s.CodDRol).ToList<string>().ToArray();
                    response.Entidad.ROLES = listroles;
                }

                if (getexcep != null && getexcep.Count() > 0)
                {
                    lisexcep = getexcep.Select(s => s.NIT).ToList<string>().ToArray();
                    response.Entidad.EXCEPCIONESNIT = lisexcep;

                    ViewBag.Exceptions = getexcep.Select(a => new SelectListItem { Text = a.NIT, Value = a.NIT });
                }
                else
                {
                   
                   
                    ViewBag.Exceptions = new List<SelectListItem>();
                }


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
        [Authorize(Roles = "Admin")]
        public ActionResult ModificarDivulgacion(Divulgacion model)
        {

            int idUser = CrossController.Instance.GetUserInfoId();
            model.CODUSUARIOINFOMODIFICACION = idUser;
            model.FECHAHORAMODIFICACION = DateTime.Now;
            try
            {
                
                int isModified = _divulgacionService.ActualizarDivulgacion(model);
                if (isModified == 1)
                {
                    //ViewBag.InstructivoMessage = "Modificado";
                }
                else
                {
                    //ViewBag.InstructivoMessage = "Error modificando divulgacion";
                }

                if(model.EXCEPCIONESNIT != null)
                {
                    List<string> excepciones = model.EXCEPCIONESNIT.ToList<string>();
                    int eliminarExcepciones = _divulgacionService.EliminarExcepcionesxDivulgacion(Int32.Parse(model.ID_DIVULGACION.ToString()));
                    foreach (var exce in excepciones)
                    {
                        int DivxExcep = _divulgacionService.AgregarExcepcionxNIT(new ExcepcionxNIT { CodDivulgacion = Int32.Parse(model.ID_DIVULGACION.ToString()), NIT = exce });
                    }
                }

                List<string> roles = model.ROLES.ToList<string>();
              
                int eliminarRoles = _divulgacionService.EliminarRolesxDivulgacion(Int32.Parse(model.ID_DIVULGACION.ToString()));
                

                foreach (var rol in roles)
                {
                    int DivxRol = _divulgacionService.AgregarDivulgacionxRol(new DivulgacionxRol { CodDivulgacion = Int32.Parse(model.ID_DIVULGACION.ToString()), CodDRol = rol });
                }
               
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }

            return RedirectToAction("Index");
        }

        public ActionResult _EliminarDivulgacion(int id)
        {

            int idUser = CrossController.Instance.GetUserInfoId();
            try
            {
                ResponseIndividualEO<Divulgacion> response = new ResponseIndividualEO<Divulgacion>();
                CriterioBusqueda Request = new CriterioBusqueda();
                Request.IdPadre = id;
                Request.IdUsuario = CrossController.Instance.GetUserInfoId();

                response = _divulgacionService.GetDetallesDivulgacion(Request);

                response.Entidad.HABILITADA = false;
                response.Entidad.CODUSUARIOINFOMODIFICACION = CrossController.Instance.GetUserInfoId();
                response.Entidad.FECHAHORAMODIFICACION = DateTime.Now;

                int isModified = _divulgacionService.ActualizarDivulgacion(response.Entidad);
                //if (isModified == 1)
                //{
                //    //ViewBag.InstructivoMessage = "Eliminado";
                //}
                //else
                //{
                //    //ViewBag.InstructivoMessage = "Error eliminando divulgacion";
                //}

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                var exception = RegistroEventos.RegistrarEvento(TipoRegistroEvento.Error, ex);
                return RedirectToAction("ErrorPage", "Account", exception);
            }

        }
    }
}