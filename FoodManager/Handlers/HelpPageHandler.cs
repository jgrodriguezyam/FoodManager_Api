using System.Web;

namespace FoodManager.Handlers
{
    public class HelpPageHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Redirect("swagger/ui/index");
        }

        public bool IsReusable { get { return true; } }
    }
}