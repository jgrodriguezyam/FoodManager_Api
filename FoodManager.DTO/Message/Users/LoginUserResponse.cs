using FoodManager.DTO.BaseResponse;

namespace FoodManager.DTO.Message.Users
{
    public class LoginUserResponse : LoginResponse
    {
        public int UserId { get; set; }
    }
}