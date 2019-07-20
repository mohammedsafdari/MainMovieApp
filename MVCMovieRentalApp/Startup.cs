using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCMovieRentalApp.Startup))]
namespace MVCMovieRentalApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
