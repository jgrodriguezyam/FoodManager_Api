using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Warnings;

namespace FoodManager.Services.Interfaces
{
    public interface IWarningService
    {
        FindWarningsResponse Find(FindWarningsRequest request);
        CreateResponse Create(WarningRequest request);
        SuccessResponse Update(WarningRequest request);
        WarningResponse Get(GetWarningRequest request);
        SuccessResponse Delete(DeleteWarningRequest request);
        SuccessResponse ChangeStatus(ChangeStatusRequest request);
    }
}