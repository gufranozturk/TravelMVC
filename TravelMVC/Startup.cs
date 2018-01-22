using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TravelMVC.Startup))]
namespace TravelMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
