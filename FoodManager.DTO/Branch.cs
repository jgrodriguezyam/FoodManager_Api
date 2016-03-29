namespace FoodManager.DTO
{
    public class Branch
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Company Company { get; set; }
        public bool Status { get; set; }
    }
}