using FoodManager.DTO.BaseRequest;

namespace FoodManager.DTO.Message.SaucerConfigurations
{
    public class FindSaucerConfigurationsRequest : FindStatusRequest
    {
        public int SaucerId { get; set; }
        public string SaucerIds { get; set; }
        public int IngredientId { get; set; }
    }
}