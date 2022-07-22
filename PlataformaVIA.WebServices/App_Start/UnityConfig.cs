namespace PlataformaVIA.WebServices
{
    using Data;
    using Data.Repositories.Implementations;
    using Data.Repositories.Interfaces;
    using Services.Implementations;
    using Services.Interfaces;
    using System.Web.Http;
    using Unity;
    using Unity.WebApi;

    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            //Business Layer 
            container.RegisterType<IInstructivoService, InstructivoService>();

            //Abstract Repository
            //container.RegisterType<IRepository<Instructivo>, ADORepository<Instructivo>>();
            container.RegisterType<IInstructivoRepository, InstructivoRepository>();
            //container.RegisterType<>

            //Fábrica de conexiones
            container.RegisterType<IConnectionFactory, DbConnectionFactory>();

            

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}