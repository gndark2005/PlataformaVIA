namespace PlataformaVIA.Presentacion
{
    using Helpers;
    using System;
    using System.Configuration;
    using System.Data.Entity;
    using System.Reflection;
    using System.Web;
    using System.Web.Configuration;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            this.VerificarRolesSuperUsuario();
            Database.SetInitializer(
              new MigrateDatabaseToLatestVersion<Models.LocalDataContext,
              Migrations.Configuration>());

            //Database.SetInitializer<ApplicationDbContext>(null);

            AreaRegistration.RegisterAllAreas();
            UnityConfig.RegisterComponents();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void VerificarRolesSuperUsuario()
        {
            UsuariosHelper.VerificarRol("Admin");
            UsuariosHelper.VerificarRol("User");
            UsuariosHelper.VerificarSuperUsuario();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            Response.Clear();

            HttpException httpException = exception as HttpException;

            int error = httpException != null ? httpException.GetHttpCode() : 0;

            Server.ClearError();
            Response.Redirect(String.Format("~/Error/?error={0}", error, exception.Message));
        }
    }
}
