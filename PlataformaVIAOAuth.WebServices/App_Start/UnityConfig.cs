namespace PlataformaVIAOAuth.WebServices
{
    using PlataformaVIA.Data;
    using PlataformaVIA.Data.Repositories.Implementations;
    using PlataformaVIA.Data.Repositories.Interfaces;
    using PlataformaVIA.Services.Implementations;
    using PlataformaVIA.Services.Interfaces;
    using System.Web.Http;
    using Unity;
    using Unity.Injection;
    using Unity.Lifetime;
    using Unity.WebApi;

    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            ////Business Layer 
            //container.RegisterType<IInstructivoService, InstructivoService>();

            ////Abstract Repository
            ////container.RegisterType<IRepository<Instructivo>, ADORepository<Instructivo>>();
            //container.RegisterType<IInstructivoRepository, InstructivoRepository>(new InjectionConstructor(new ResolvedParameter<DbContext>("context")));
            ////container.RegisterType<>

            ////Fábrica de conexiones
            //container.RegisterType<IConnectionFactory, DbConnectionFactory>();

            container.RegisterType<IPuntoVentaService, PuntoVentaService>();
            container.RegisterType<ISeguridadService, SeguridadService>();
            container.RegisterType<IAdministracionCorreoService, AdministracionCorreoService>();
            container.RegisterType<ICadenaService, CadenaService>();
            container.RegisterType<IViaBalotoService, ViaBalotoService>();
            container.RegisterType<IAppMobileService, AppMobileService>();
            container.RegisterType<IDivulgacionService, DivulgacionService>();



            //Repositorios
            //container.RegisterType<IPuntoVentaRepository, PuntoVentaRepository>(new HierarchicalLifetimeManager(),new InjectionConstructor(new ResolvedParameter<DbContext>("context")));
            //container.RegisterType<ICadenaRepository, CadenaRepository>(new HierarchicalLifetimeManager(),new InjectionConstructor(new ResolvedParameter<DbContext>("context")));
            container.RegisterType<IPuntoVentaRepository, PuntoVentaRepository>();
            container.RegisterType<ICadenaRepository, CadenaRepository>();
            container.RegisterType<ISeguridadRepository, SeguridadRepository>();
            container.RegisterType<IAdministracionCorreoRepository, AdministracionCorreoRepository>();
            container.RegisterType<IViaBalotoRepository, ViaBalotoRepository>();
            container.RegisterType<IAppMobileRepository, AppMobileRepository>();

            container.RegisterType<INotificacionesRepository, NotificacionesRepository>();
            container.RegisterType<ISolicitudesBatchRepository, SolicitudesBatchRepository>();

            container.RegisterType<ICicloFacturacionRepository, CicloFacturacionRepository>();
            container.RegisterType<IColocadorRepository, ColocadorRepository>();
            container.RegisterType<IDivulgacionRepository, DivulgacionRepository>();

            //Fábrica de conexiones
            container.RegisterType<IConnectionFactory, DbConnectionFactory>(new HierarchicalLifetimeManager());
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}