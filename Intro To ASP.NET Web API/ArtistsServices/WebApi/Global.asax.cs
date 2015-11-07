namespace ArtistsServices.WebApi
{
    using System.Reflection;
    using System.Web;
    using System.Web.Http;

    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            DatabaseConfig.Initialize();
            AutomapperConfig.RegisterMappings(Assembly.Load("WebApi"));
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
