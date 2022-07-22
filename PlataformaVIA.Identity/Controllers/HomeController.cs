namespace PlataformaVIA.Identity.Controllers
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using PlataformaVIA.Identity.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            
            //var email = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId()).Email;

            //TODO Consultar Web API para traer datos por email

            return View();
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

        public ActionResult _Carousel()
        {
            return PartialView();
        }
    }
}