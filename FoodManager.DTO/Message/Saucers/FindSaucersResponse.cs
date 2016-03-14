using System.Collections.Generic;
using FoodManager.DTO.BaseResponse;

namespace FoodManager.DTO.Message.Saucers
{
    public class FindSaucersResponse : FindBaseResponse
    {
        public FindSaucersResponse()
        {
            Saucers = new List<SaucerResponse>();
        }

        public List<SaucerResponse> Saucers { get; set; } 
    }
}