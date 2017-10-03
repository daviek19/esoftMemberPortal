using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EsoftPortalMvc.Startup))]
namespace EsoftPortalMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
