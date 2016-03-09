using System.Collections.Generic;
using FoodManager.DTO.BaseResponse;

namespace FoodManager.DTO.Message.Warnings
{
    public class FindWarningsResponse : FindBaseResponse
    {
        public FindWarningsResponse()
        {
            Warnings = new List<WarningResponse>();
        }

        public List<WarningResponse> Warnings { get; set; } 
    }
}