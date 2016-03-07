using FoodManager.DTO;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Users;

namespace FoodManager.Services.Interfaces
{
    public interface IUserService
    {
        FindUsersResponse Find(FindUsersRequest request);
        CreateResponse Create(UserRequest request);
        SuccessResponse Update(UserRequest request);
        User Get(GetUserRequest request);
        SuccessResponse Delete(DeleteUserRequest request);
        LoginUserResponse Login(LoginUserRequest request);
        SuccessResponse Logout(LogoutUserRequest request);
        SuccessResponse ChangePassword(ChangeUserPasswordRequest request);
    }
}