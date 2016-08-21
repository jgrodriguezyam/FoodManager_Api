namespace FoodManager.DTO.Message.Branches
{
    public class BranchRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int RegionId { get; set; }
        public int CompanyId { get; set; }
    }
}