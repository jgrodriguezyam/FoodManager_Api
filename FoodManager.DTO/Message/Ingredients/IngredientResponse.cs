using FoodManager.DTO.Message.IngredientGroups;
using FoodManager.Infrastructure.Application;

namespace FoodManager.DTO.Message.Ingredients
{
    public class IngredientResponse : NutritionInformation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal NetWeight { get; set; }
        public int Unit { get; set; }
        public IngredientGroupResponse IngredientGroup { get; set; }
        public bool Status { get; set; }
        public bool IsReference { get; set; }
    }
}