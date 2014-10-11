using MediaCommMvc.Web;

using Microsoft.Owin;

using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace MediaCommMvc.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}
