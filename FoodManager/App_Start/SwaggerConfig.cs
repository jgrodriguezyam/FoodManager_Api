using System.Web.Http;
using Swashbuckle.Application;
using FoodManager.Filters;

namespace FoodManager
{
    public class SwaggerConfig
    {
        public static void Register(HttpConfiguration configuration)
        {
            configuration
                .EnableSwagger(configure =>
                {
                    configure.SingleApiVersion("v1", "FoodManager");
                    configure.OperationFilter<SwaggerOperationFilter>();
                })
                .EnableSwaggerUi();
        }
    }
}