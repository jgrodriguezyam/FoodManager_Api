using System.Collections.Generic;
using FoodManager.DTO.BaseResponse;

namespace FoodManager.DTO.Message.Tips
{
    public class FindTipsResponse : FindBaseResponse
    {
        public FindTipsResponse()
        {
            Tips = new List<TipResponse>();
        }

        public List<TipResponse> Tips { get; set; } 
    }
}