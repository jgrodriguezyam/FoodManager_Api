using FoodManager.DTO.BaseRequest;

namespace FoodManager.DTO.Message.Users
{
    public class FindUsersRequest : FindBaseRequest
    {
        public string Name { get; set; }
        public string UserName { get; set; }
    }
}