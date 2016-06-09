using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Saucers;
using FoodManager.Infrastructure.Application;

namespace FoodManager.Services.Interfaces
{
    public interface ISaucerService
    {
        FindSaucersResponse Find(FindSaucersRequest request);
        CreateResponse Create(SaucerRequest request);
        SuccessResponse Update(SaucerRequest request);
        SaucerResponse Get(GetSaucerRequest request);
        SuccessResponse Delete(DeleteSaucerRequest request);
        SuccessResponse ChangeStatus(ChangeStatusRequest request);
        SuccessResponse IsReference(IsReferenceRequest request);
        NutritionInformation GetNutritionInformation(GetNutritionInformationRequest request);
    }
}