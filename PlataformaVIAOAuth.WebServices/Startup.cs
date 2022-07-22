using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(PlataformaVIAOAuth.WebServices.Startup))]
namespace PlataformaVIAOAuth.WebServices
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
