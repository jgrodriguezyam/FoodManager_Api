using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Saucers;
using FoodManager.Infrastructure.Application;
using FoodManager.Infrastructure.Files;

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
        SuccessResponse Csv(CsvRequest request, File file);
    }
}