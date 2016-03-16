﻿using FoodManager.DTO.BaseRequest;

namespace FoodManager.DTO.Message.Ingredients
{
    public class FindIngredientsRequest : FindStatusRequest
    {
        public int IngredientGroupId { get; set; }
    }
}