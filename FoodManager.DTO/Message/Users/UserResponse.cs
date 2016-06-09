using FoodManager.DTO.Message.Dealers;
using FoodManager.DTO.Message.Roles;

namespace FoodManager.DTO.Message.Users
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DealerResponse Dealer { get; set; }
        public RoleResponse Role { get; set; }
        public bool Status { get; set; }
    }
}