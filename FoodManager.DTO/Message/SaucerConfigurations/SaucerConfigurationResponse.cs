using FoodManager.DTO.Message.Ingredients;
using FoodManager.DTO.Message.Saucers;

namespace FoodManager.DTO.Message.SaucerConfigurations
{
    public class SaucerConfigurationResponse
    {
        public int Id { get; set; }
        public SaucerResponse Saucer { get; set; }
        public IngredientResponse Ingredient { get; set; }
        public decimal NetWeight { get; set; }
        public bool Status { get; set; }
    }
}