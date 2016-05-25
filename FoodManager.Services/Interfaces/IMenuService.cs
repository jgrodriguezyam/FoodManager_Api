using FoodManager.DTO;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Menus;

namespace FoodManager.Services.Interfaces
{
    public interface IMenuService
    {
        FindMenusResponse Find(FindMenusRequest request);
        CreateResponse Create(MenuRequest request);
        SuccessResponse Update(MenuRequest request);
        Menu Get(GetMenuRequest request);
        SuccessResponse Delete(DeleteMenuRequest request);
        SuccessResponse ChangeStatus(ChangeStatusRequest request);
    }
}