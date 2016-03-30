using FoodManager.DTO.BaseRequest;

namespace FoodManager.DTO.Message.Dealers
{
    public class FindDealersRequest : FindStatusRequest
    {
        public string Name { get; set; }
    }
}