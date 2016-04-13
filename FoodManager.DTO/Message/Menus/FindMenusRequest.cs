using FoodManager.DTO.BaseRequest;

namespace FoodManager.DTO.Message.Menus
{
    public class FindMenusRequest : FindStatusRequest
    {
        public int DealerId { get; set; }
        public int SaucerId { get; set; }
        public bool OnlyToday { get; set; }
    }
}