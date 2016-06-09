using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Users;

namespace FoodManager.Services.Interfaces
{
    public interface IUserService
    {
        FindUsersResponse Find(FindUsersRequest request);
        CreateResponse Create(UserRequest request);
        SuccessResponse Update(UserRequest request);
        UserResponse Get(GetUserRequest request);
        SuccessResponse Delete(DeleteUserRequest request);
        LoginUserResponse Login(LoginUserRequest request);
        SuccessResponse Logout(LogoutUserRequest request);
        SuccessResponse ChangePassword(ChangeUserPasswordRequest request);
        SuccessResponse ChangeStatus(ChangeStatusRequest request);
    }
}