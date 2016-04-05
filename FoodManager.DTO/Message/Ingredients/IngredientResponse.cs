using FoodManager.Infrastructure.Application;

namespace FoodManager.DTO.Message.Ingredients
{
    public class IngredientResponse : NutritionInformation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public int IngredientGroupId { get; set; }
        public bool Status { get; set; }
    }
}