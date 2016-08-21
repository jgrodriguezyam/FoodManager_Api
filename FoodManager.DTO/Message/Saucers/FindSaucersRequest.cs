using FoodManager.DTO.BaseRequest;

namespace FoodManager.DTO.Message.Saucers
{
    public class FindSaucersRequest : FindStatusRequest
    {
        public string Name { get; set; }
        public int Type { get; set; }
    }
}