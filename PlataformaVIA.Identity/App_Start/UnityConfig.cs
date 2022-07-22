namespace PlataformaVIA.Identity
{
    using Services.Interfaces;
    using Services.Implementations;
    using System.Web.Mvc;
    using Unity;
    using Unity.Mvc5;
    using PlataformaVIA.Data.Repositories.Interfaces;
    using PlataformaVIA.Data.Repositories.Implementations;
    using Unity.Injection;
    using PlataformaVIA.Data;
    using PlataformaVIA.Identity.Controllers;

    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();


            container.RegisterType<AccountController>(new InjectionConstructor());
            //container.RegisterType<RolesAdminController>(new InjectionConstructor());
            container.RegisterType<ManageController>(new InjectionConstructor());
            //container.RegisterType<UsersAdminController>(new InjectionConstructor());

            //Capa de negocio

            container.RegisterType<IPuntoVentaService, PuntoVentaService>();


            //Repositorios
            container.RegisterType<IPuntoVentaService, PuntoVentaService>();


            container.RegisterType<IPuntoVentaRepository, PuntoVentaRepository>(new InjectionConstructor(new ResolvedParameter<DbContext>("context")));

            //Fábrica de conexiones
            container.RegisterType<IConnectionFactory, DbConnectionFactory>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}