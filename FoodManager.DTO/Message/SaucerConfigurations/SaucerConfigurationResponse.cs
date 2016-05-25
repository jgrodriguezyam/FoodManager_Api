namespace FoodManager.DTO.Message.SaucerConfigurations
{
    public class SaucerConfigurationResponse
    {
        public int Id { get; set; }
        public int SaucerId { get; set; }
        public int IngredientId { get; set; }
        public decimal Portion { get; set; }
        public bool Status { get; set; }
    }
}