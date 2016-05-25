using System.Collections.Generic;
using FoodManager.DTO.BaseResponse;

namespace FoodManager.DTO.Message.Ingredients
{
    public class FindIngredientsResponse : FindBaseResponse
    {
        public FindIngredientsResponse()
        {
            Ingredients = new List<IngredientResponse>();
        }

        public List<IngredientResponse> Ingredients { get; set; } 
    }
}