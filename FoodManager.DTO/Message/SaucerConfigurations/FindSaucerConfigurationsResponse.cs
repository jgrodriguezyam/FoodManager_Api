using System.Collections.Generic;
using FoodManager.DTO.BaseResponse;

namespace FoodManager.DTO.Message.SaucerConfigurations
{
    public class FindSaucerConfigurationsResponse : FindBaseResponse
    {
        public FindSaucerConfigurationsResponse()
        {
            SaucerConfigurations = new List<SaucerConfigurationResponse>();
        }

        public List<SaucerConfigurationResponse> SaucerConfigurations { get; set; } 
    }
}