using FoodManager.DTO.BaseRequest;

namespace FoodManager.DTO.Message.Tips
{
    public class FindTipsRequest : FindStatusRequest
    {
        public string Name { get; set; }
    }
}