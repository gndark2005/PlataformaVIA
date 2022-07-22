using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PlataformaVIA.Identity.Startup))]
namespace PlataformaVIA.Identity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
