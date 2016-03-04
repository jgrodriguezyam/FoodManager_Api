using System.Collections.Generic;
using FoodManager.DTO.BaseResponse;

namespace FoodManager.DTO.Message.Regions
{
    public class FindRegionsResponse : FindBaseResponse
    {
        public FindRegionsResponse()
        {
            Regions = new List<RegionResponse>();
        }

        public List<RegionResponse> Regions { get; set; } 
    }
}