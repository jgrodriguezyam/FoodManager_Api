using FoodManager.DTO.BaseRequest;

namespace FoodManager.DTO.Message.Branches
{
    public class FindBranchesRequest : FindStatusRequest
    {
        public int RegionId { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}