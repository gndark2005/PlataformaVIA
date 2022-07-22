using PlataformaVIA.Data.Repositories.Implementations;
using PlataformaVIA.Data.Repositories.Interfaces;
using PlataformaVIA.Core.Domain.Media;
using PlataformaVIA.Data.Repositories;
using PlataformaVIA.Services.Implementations;
using PlataformaVIA.Services.Interfaces;
using System.Web.Http;
using Unity;
using Unity.WebApi;
using PlataformaVIA.Core.Data;

namespace PlataformaVIA.Web
{
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
            //container.RegisterType<IInstructivoRepository, InstructivoRepository>();
            //container.RegisterType<>



            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}