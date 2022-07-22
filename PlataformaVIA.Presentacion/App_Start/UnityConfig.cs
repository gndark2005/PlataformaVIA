namespace PlataformaVIA.Presentacion
{
    using Data;
    using Data.Repositories.Implementations;
    using Data.Repositories.Interfaces;
    using Presentacion.Controllers;
    using Services.Implementations;
    using Services.Interfaces;
    using System.Web.Mvc;
    using Unity;
    using Unity.Injection;
    using Unity.Lifetime;
    using Unity.Mvc5;

    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();


            
            //container.RegisterType<RolesAdminController>(new InjectionConstructor());
            container.RegisterType<ManageController>(new InjectionConstructor());
            //container.RegisterType<UsersAdminController>(new InjectionConstructor());

            //Capa de negocio

            container.RegisterType<IPuntoVentaService, PuntoVentaService>();
            container.RegisterType<ICadenaService, CadenaService>();
            container.RegisterType<ICicloFacturacionService, CicloFacturacionService>();
            container.RegisterType<IPagoService, PagoService>();
            container.RegisterType<IAjusteService, AjusteService>();
            container.RegisterType<IColocadorService, ColocadorService>();
            container.RegisterType<ISeguridadService, SeguridadService>();
            container.RegisterType<ITerminalService, TerminalService>();
            container.RegisterType<IAdministracionCorreoService, AdministracionCorreoService>();
            container.RegisterType<ICertificadosService, CertificadosService>();
            container.RegisterType<IDocumentosService, DocumentosService>();
            container.RegisterType<INotificacionesService, NotificacionesService>();
            container.RegisterType<IAdministradorDocumentosService, AdministradorDocumentosService>();
            container.RegisterType<IParametroService, ParametroService>();
            container.RegisterType<IDivulgacionService, DivulgacionService>();


            //Repositorios
            //container.RegisterType<IPuntoVentaRepository, PuntoVentaRepository>(new HierarchicalLifetimeManager(),new InjectionConstructor(new ResolvedParameter<DbContext>("context")));
            //container.RegisterType<ICadenaRepository, CadenaRepository>(new HierarchicalLifetimeManager(),new InjectionConstructor(new ResolvedParameter<DbContext>("context")));
            container.RegisterType<ITerminalRepository, TerminalRepository>();
            container.RegisterType<IPuntoVentaRepository, PuntoVentaRepository>();
            container.RegisterType<ICadenaRepository, CadenaRepository>();
            container.RegisterType<ICicloFacturacionRepository, CicloFacturacionRepository>();
            container.RegisterType<IPagoRepository, PagoRepository>();
            container.RegisterType<IAjusteRepository, AjusteRepository>();
            container.RegisterType<IColocadorRepository, ColocadorRepository>();
            container.RegisterType<ISeguridadRepository, SeguridadRepository>();
            container.RegisterType<IAdministracionCorreoRepository, AdministracionCorreoRepository>();
            container.RegisterType<ICertificadoRepository, CertificadoRepository>();
            container.RegisterType<IDocumentosRepository, DocumentosRepository>();
            container.RegisterType<INotificacionesRepository, NotificacionesRepository>();
            container.RegisterType<IAdministradorDocumentosRepository, AdministradorDocumentosRepository>();
            container.RegisterType<IParametroRepository, ParametroRepository>();
            container.RegisterType<IDivulgacionRepository, DivulgacionRepository>();


            //Fábrica de conexiones
            container.RegisterType<IConnectionFactory, DbConnectionFactory>(new HierarchicalLifetimeManager());
            container.RegisterType<AccountController>(new InjectionConstructor(container.Resolve<IAdministracionCorreoService>(), container.Resolve<ISeguridadService>()));
            container.RegisterType<ManageController>(new InjectionConstructor(container.Resolve<IAdministracionCorreoService>(), container.Resolve<ITerminalService>(), container.Resolve<ISeguridadService>()));
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}