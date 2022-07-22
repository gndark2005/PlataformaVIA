namespace PlataformaVIAOAuth.WebServices.Controllers
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using System.Web;
    using System.Web.Mvc;
    using WebServices.Models;

    public class CommonController : Controller
    {
        // GET: Common
        public  ActionResult HeaderUserInfo()
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            if (user != null)
            {
                return PartialView(user);
            }
            
                return PartialView();
            
        }
    }
}