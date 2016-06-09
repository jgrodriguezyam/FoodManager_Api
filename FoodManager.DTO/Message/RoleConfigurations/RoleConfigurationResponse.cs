using FoodManager.DTO.Message.AccessLevels;
using FoodManager.DTO.Message.Permissions;
using FoodManager.DTO.Message.Roles;

namespace FoodManager.DTO.Message.RoleConfigurations
{
    public class RoleConfigurationResponse
    {
        public int Id { get; set; }
        public RoleResponse Role { get; set; }
        public PermissionResponse Permission { get; set; }
        public AccessLevelResponse AccessLevel { get; set; }
    }
}