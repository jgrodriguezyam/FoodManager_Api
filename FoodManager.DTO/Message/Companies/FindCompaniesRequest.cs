using FoodManager.DTO.BaseRequest;

namespace FoodManager.DTO.Message.Companies
{
    public class FindCompaniesRequest : FindStatusRequest
    {
        public string Name { get; set; }
    }
}