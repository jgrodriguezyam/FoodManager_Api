using FoodManager.Infrastructure.Application;

namespace FoodManager.DTO
{
    public class Ingredient : NutritionInformation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public IngredientGroup IngredientGroup { get; set; }
        public bool Status { get; set; }
    }
}