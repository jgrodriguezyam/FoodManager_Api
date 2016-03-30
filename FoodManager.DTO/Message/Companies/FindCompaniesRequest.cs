using FoodManager.DTO.BaseRequest;

namespace FoodManager.DTO.Message.Companies
{
    public class FindCompaniesRequest : FindStatusRequest
    {
        public int RegionId { get; set; }
        public string Name { get; set; }
    }
}