using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PlataformaVIAbORRAR.Startup))]
namespace PlataformaVIAbORRAR
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
