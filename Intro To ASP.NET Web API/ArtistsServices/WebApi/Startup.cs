using ArtistsServices.WebApi;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace ArtistsServices.WebApi
{
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
