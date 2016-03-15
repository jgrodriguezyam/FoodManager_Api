using System.Collections.Generic;
using FoodManager.DTO.BaseResponse;

namespace FoodManager.DTO.Message.IngredientGroups
{
    public class FindIngredientGroupsResponse : FindBaseResponse
    {
        public FindIngredientGroupsResponse()
        {
            IngredientGroups = new List<IngredientGroupResponse>();
        }

        public List<IngredientGroupResponse> IngredientGroups { get; set; } 
    }
}