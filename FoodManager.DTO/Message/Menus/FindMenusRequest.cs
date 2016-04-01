using FoodManager.DTO.BaseRequest;

namespace FoodManager.DTO.Message.Menus
{
    public class FindMenusRequest : FindStatusRequest
    {
        public string Name { get; set; }
        public int DealerId { get; set; }
        public int SaucerId { get; set; }
    }
}