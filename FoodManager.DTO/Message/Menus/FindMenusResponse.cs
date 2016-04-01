using System.Collections.Generic;
using FoodManager.DTO.BaseResponse;

namespace FoodManager.DTO.Message.Menus
{
    public class FindMenusResponse : FindBaseResponse
    {
        public FindMenusResponse()
        {
            Menus = new List<MenuResponse>();
        }

        public List<MenuResponse> Menus { get; set; } 
    }
}