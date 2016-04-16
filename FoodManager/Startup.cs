using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using FoodManager.IoC.Configs;
using FoodManager.Mapper.Configs;
using FoodManager.Sessions;

[assembly: OwinStartup(typeof(FoodManager.Startup))]
namespace FoodManager
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var configuration = new HttpConfiguration(GlobalConfiguration.Configuration.Routes);
            SwaggerConfig.Register(configuration);
            FilterConfig.Register(configuration);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            appBuilder.UseCors(CorsOptions.AllowAll);
            appBuilder.UseWebApi(configuration);
            SimpleInjectorConfig.Register(configuration);
            GlobalConfiguration.Configuration.Filters.Add(new AuthorizeLogin());
            FastMapperConfig.Initialize();
        }
    }
}
