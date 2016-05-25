using System.Collections.Generic;
using FoodManager.DTO.BaseResponse;

namespace FoodManager.DTO.Message.Dealers
{
    public class FindDealersResponse : FindBaseResponse
    {
        public FindDealersResponse()
        {
            Dealers = new List<DealerResponse>();
        }

        public List<DealerResponse> Dealers { get; set; } 
    }
}