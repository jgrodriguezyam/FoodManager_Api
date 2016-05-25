using FoodManager.DTO.BaseRequest;

namespace FoodManager.DTO.Message.Roles
{
    public class FindRolesRequest : FindStatusRequest
    {
        public string Name { get; set; }
    }
}