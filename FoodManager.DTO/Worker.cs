namespace FoodManager.DTO
{
    public class Worker
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Imss { get; set; }
        public int Gender { get; set; }
        public string Badge { get; set; }
        public Department Department { get; set; }
        public Job Job { get; set; }
        public Role Role { get; set; }
        public Branch Branch { get; set; }
        public bool Status { get; set; }
    }
}