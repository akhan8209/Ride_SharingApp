using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ride_SharingApp.Startup))]
namespace Ride_SharingApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
