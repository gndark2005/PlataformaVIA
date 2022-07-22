namespace PlataformaVIA.Identity
{
    using Helpers;
    using System.Data.Entity;
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
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            UnityConfig.RegisterComponents();
        }

        private void VerificarRolesSuperUsuario()
        {
            UsuariosHelper.VerificarRol("Admin");
            UsuariosHelper.VerificarRol("User");
            UsuariosHelper.VerificarSuperUsuario();
        }
    }
}
