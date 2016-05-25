namespace FoodManager.DTO
{
    public class RoleConfiguration
    {
        public int Id { get; set; }
        public Role Role { get; set; }
        public Permission Permission { get; set; }
        public AccessLevel AccessLevel { get; set; }
    }
}