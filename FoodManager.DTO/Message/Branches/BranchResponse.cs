using FoodManager.DTO.Message.Companies;

namespace FoodManager.DTO.Message.Branches
{
    public class BranchResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public CompanyResponse Company { get; set; }
        public bool Status { get; set; }
    }
}