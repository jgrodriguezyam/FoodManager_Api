using FoodManager.DTO.BaseRequest;

namespace FoodManager.DTO.Message.Ingredients
{
    public class FindIngredientsRequest : FindStatusRequest
    {
        public int IngredientGroupId { get; set; }
        public string Name { get; set; }
        public string WithoutIds { get; set; }
        public int Unit { get; set; }
    }
}