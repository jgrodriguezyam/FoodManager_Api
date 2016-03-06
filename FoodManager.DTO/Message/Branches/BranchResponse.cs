namespace FoodManager.DTO.Message.Branches
{
    public class BranchResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int CompanyId { get; set; }
        public bool Status { get; set; }
    }
}