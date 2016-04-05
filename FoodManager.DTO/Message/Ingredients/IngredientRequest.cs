using FoodManager.Infrastructure.Application;

namespace FoodManager.DTO.Message.Ingredients
{
    public class IngredientRequest : NutritionInformation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public int IngredientGroupId { get; set; }
    }
}