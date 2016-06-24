using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace FoodManager.Infrastructure.Http
{
    public static class HttpActionExtensions
    {
        public static string GetValue(this HttpActionContext actionContext, string key)
        {
            return actionContext.Request.Headers.GetValues(key).First();
        }

        public static string GetValue(this HttpActionExecutedContext actionExecutedContext, string key)
        {
            return actionExecutedContext.Request.Headers.GetValues(key).First();
        }
    }
}