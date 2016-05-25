using FoodManager.DTO.BaseRequest;

namespace FoodManager.DTO.Message.RoleConfigurations
{
    public class FindRoleConfigurationsRequest : FindBaseRequest
    {
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        public int AccessLevelId { get; set; }
    }
}