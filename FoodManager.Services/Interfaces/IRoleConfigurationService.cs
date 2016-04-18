using FoodManager.DTO;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.AccessLevels;
using FoodManager.DTO.Message.RoleConfigurations;

namespace FoodManager.Services.Interfaces
{
    public interface IRoleConfigurationService
    {
        FindRoleConfigurationsResponse Find(FindRoleConfigurationsRequest request);
        CreateResponse Create(RoleConfigurationRequest request);
        SuccessResponse Update(RoleConfigurationRequest request);
        RoleConfiguration Get(GetRoleConfigurationRequest request);
        SuccessResponse Delete(DeleteRoleConfigurationRequest request);
        FindAccessLevelsResponse Find(FindAccessLevelsRequest request);
    }
}