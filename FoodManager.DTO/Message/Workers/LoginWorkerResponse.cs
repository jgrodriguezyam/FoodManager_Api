using FoodManager.DTO.BaseResponse;

namespace FoodManager.DTO.Message.Workers
{
    public class LoginWorkerResponse : LoginResponse
    {
        public int WorkerId { get; set; }
    }
}