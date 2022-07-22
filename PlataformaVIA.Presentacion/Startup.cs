using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PlataformaVIA.Presentacion.Startup))]
namespace PlataformaVIA.Presentacion
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
