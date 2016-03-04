using System.Web.Http;
using FoodManager.Filters;

namespace FoodManager
{
    public class FilterConfig
    {
        public static void Register(HttpConfiguration configuration)
        {
            configuration.Filters.Add(new ExceptionFilter());
        }
    }
}
