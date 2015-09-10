using MediaCommMvc.Web;
using MediaCommMvc.Web.App_Start;

using Microsoft.Owin;

using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace MediaCommMvc.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AuthenticationConfig.ConfigureAuth(app);
        }
    }
}
