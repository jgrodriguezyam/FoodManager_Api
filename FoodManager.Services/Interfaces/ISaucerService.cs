using FoodManager.DTO;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Saucers;

namespace FoodManager.Services.Interfaces
{
    public interface ISaucerService
    {
        FindSaucersResponse Find(FindSaucersRequest request);
        CreateResponse Create(SaucerRequest request);
        SuccessResponse Update(SaucerRequest request);
        Saucer Get(GetSaucerRequest request);
        SuccessResponse Delete(DeleteSaucerRequest request);
        SuccessResponse ChangeStatus(ChangeStatusRequest request);
        SuccessResponse IsReference(IsReferenceRequest request);
    }
}