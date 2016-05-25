using FoodManager.DTO.BaseRequest;

namespace FoodManager.DTO.Message.Diseases
{
    public class FindDiseasesRequest : FindStatusRequest
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}