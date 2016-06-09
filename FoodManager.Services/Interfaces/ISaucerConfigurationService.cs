using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.SaucerConfigurations;

namespace FoodManager.Services.Interfaces
{
    public interface ISaucerConfigurationService
    {
        FindSaucerConfigurationsResponse Find(FindSaucerConfigurationsRequest request);
        CreateResponse Create(SaucerConfigurationRequest request);
        SuccessResponse Update(SaucerConfigurationRequest request);
        SaucerConfigurationResponse Get(GetSaucerConfigurationRequest request);
        SuccessResponse Delete(DeleteSaucerConfigurationRequest request);
        SuccessResponse ChangeStatus(ChangeStatusRequest request);
    }
}