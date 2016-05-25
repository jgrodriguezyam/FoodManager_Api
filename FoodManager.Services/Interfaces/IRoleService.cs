using FoodManager.DTO;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Roles;

namespace FoodManager.Services.Interfaces
{
    public interface IRoleService
    {
        FindRolesResponse Find(FindRolesRequest request);
        CreateResponse Create(RoleRequest request);
        SuccessResponse Update(RoleRequest request);
        Role Get(GetRoleRequest request);
        SuccessResponse Delete(DeleteRoleRequest request);
        SuccessResponse ChangeStatus(ChangeStatusRequest request);
        SuccessResponse IsReference(IsReferenceRequest request);
    }
}