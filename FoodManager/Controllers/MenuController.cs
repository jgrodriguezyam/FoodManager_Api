using System.Web.Http;
using FoodManager.DTO;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Menus;
using FoodManager.Infrastructure.Dates.Enums;
using FoodManager.Infrastructure.Enums;
using FoodManager.Services.Interfaces;

namespace FoodManager.Controllers
{
    public class MenuController : ApiController
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet, Route("menus")]
        public FindMenusResponse Get(FindMenusRequest request)
        {
            return _menuService.Find(request);
        }

        [HttpPost, Route("menus")]
        public CreateResponse Post(MenuRequest request)
        {
            return _menuService.Create(request);
        }

        [HttpPut, Route("menus")]
        public SuccessResponse Put(MenuRequest request)
        {
            return _menuService.Update(request);
        }

        [HttpGet, Route("menus/{Id}")]
        public Menu Get(GetMenuRequest request)
        {
            return _menuService.Get(request);
        }

        [HttpDelete, Route("menus/{Id}")]
        public SuccessResponse Delete(DeleteMenuRequest request)
        {
            return _menuService.Delete(request);
        }

        [HttpPut, Route("menus/{Id}/status/{Status}")]
        public SuccessResponse Put(ChangeStatusRequest request)
        {
            return _menuService.ChangeStatus(request);
        }

        [HttpGet, Route("menus/daysofweek")]
        public EnumeratorResponse Get()
        {
            return new EnumeratorResponse { Enumerator = new DayWeek().ConvertToCollection() };
        }
    }
}