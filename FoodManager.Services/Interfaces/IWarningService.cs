using FoodManager.DTO;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Warnings;

namespace FoodManager.Services.Interfaces
{
    public interface IWarningService
    {
        FindWarningsResponse Find(FindWarningsRequest request);
        CreateResponse Create(WarningRequest request);
        SuccessResponse Update(WarningRequest request);
        Warning Get(GetWarningRequest request);
        SuccessResponse Delete(DeleteWarningRequest request);
    }
}