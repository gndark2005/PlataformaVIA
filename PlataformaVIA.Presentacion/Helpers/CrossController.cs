namespace PlataformaVIA.Presentacion.Helpers
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Presentacion.Models;
    using System.Web;


    internal class CrossController
    {
        private static CrossController _instance;
        public static CrossController Instance { get {
                if (_instance == null) {
                    _instance = new CrossController();
                }
                return _instance;
            } }



        internal int GetUserInfoId()
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            if (user != null)
            {
                return user.UsuarioInfo.ID_USUARIOINFO;
            }
            else
            {
                return 0;
            }
        }

        internal string GetUserInfoEmail()
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            if (user != null)
            {
                return user.UsuarioInfo.EMAIL;
            }
            else
            {
                return "";
            }
        }
    }
}