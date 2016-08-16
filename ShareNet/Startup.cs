using Microsoft.Owin;
using Owin;
using ShareNet.Web;

[assembly: OwinStartup(typeof(Startup))]
namespace ShareNet.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }


}
