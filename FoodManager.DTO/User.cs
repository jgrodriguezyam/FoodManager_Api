namespace FoodManager.DTO
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int DealerId { get; set; }
        public bool Status { get; set; }
    }
}