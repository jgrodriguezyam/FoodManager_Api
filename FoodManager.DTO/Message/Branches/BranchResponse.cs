using FoodManager.DTO.Message.Companies;
using FoodManager.DTO.Message.Regions;

namespace FoodManager.DTO.Message.Branches
{
    public class BranchResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public RegionResponse Region { get; set; }
        public CompanyResponse Company { get; set; }
        public bool Status { get; set; }
        public bool IsReference { get; set; }
    }
}