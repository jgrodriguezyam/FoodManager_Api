using System.Web.Http;
using FoodManager.DTO.BaseResponse;

namespace FoodManager.Controllers
{
    public class IsAliveController : ApiController
    {
        [HttpGet, Route("alive")]
        public SuccessResponse IsAlive()
        {
            return new SuccessResponse { IsSuccess = true };
        }
    }
}