using FoodManager.DTO.BaseRequest;

namespace FoodManager.DTO.Message.Regions
{
    public class FindRegionsRequest : FindStatusRequest
    {
        public string Name { get; set; }
    }
}