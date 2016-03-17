namespace FoodManager.DTO.Message.SaucerConfigurations
{
    public class SaucerConfigurationRequest
    {
        public int Id { get; set; }
        public int SaucerId { get; set; }
        public int IngredientId { get; set; }
        public int Amount { get; set; }
    }
}