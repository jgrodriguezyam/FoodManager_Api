using FoodManager.DTO.BaseRequest;

namespace FoodManager.DTO.Message.IngredientGroups
{
    public class FindIngredientGroupsRequest : FindStatusRequest
    {
        public string Name { get; set; }
    }
}