using PlataformaVIA.Presentacion.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlataformaVIA.Presentacion.Controllers
{
    [SecurityLog]
    public class ErrorController : Controller
    {
        public ActionResult Index(int error = 0)
        {
            switch (error)
            {
                case 505:
                    ViewBag.Title = "Ocurrio un error inesperado";
                    ViewBag.Description = "Algo ha salido mal al realizar la operación solicitada.";
                    break;

                case 404:
                    ViewBag.Title = "Página no encontrada";
                    ViewBag.Description = "La URL que está intentando ingresar no existe";
                    break;

                default:
                    ViewBag.Title = "Error";
                    ViewBag.Description = "No se ha podido realizar correctamente la solicitud";
                    break;
            }

            return View("~/Views/Error/ErrorPage.cshtml");
        }
    }
}