using System.Web.Http;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;

namespace FoodManager.IoC.Configs
{
    public static class SimpleInjectorConfig
    {
        public static void Register(HttpConfiguration configuration)
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();
            SimpleInjectorModule.SetContainer(container);
            SimpleInjectorModule.Load();
            container.RegisterWebApiControllers(configuration);
            SimpleInjectorModule.VerifyContainer();
            configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}