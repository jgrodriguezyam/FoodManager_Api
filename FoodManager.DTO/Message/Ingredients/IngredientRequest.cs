using FoodManager.Infrastructure.Application;

namespace FoodManager.DTO.Message.Ingredients
{
    public class IngredientRequest : NutritionInformation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public decimal NetWeight { get; set; }
        public int Unit { get; set; }
        public int IngredientGroupId { get; set; }
    }
}