using System.Collections.Generic;
using FoodManager.DTO.BaseResponse;

namespace FoodManager.DTO.Message.RoleConfigurations
{
    public class FindRoleConfigurationsResponse : FindBaseResponse
    {
        public FindRoleConfigurationsResponse()
        {
            RoleConfigurations = new List<RoleConfigurationResponse>();
        }

        public List<RoleConfigurationResponse> RoleConfigurations { get; set; } 
    }
}